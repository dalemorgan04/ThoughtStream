/// <reference path="../../framework/jquery-ui-1.12.1.js" />
/// <reference path="../../framework/jquery-1.10.2.intellisense.js" />
/// <reference path="../../framework/timedropper.js" />
/// <reference path="../../framework/monthpicker.js" />
/// <reference path="../../framework/moment.js" />
/// <reference path="../../framework/layout.js" />

$(function () {
    tasksPubsub.init();
});

var tasksPubsub = {

    urls: {

        getAddTask: urlsPubsub.getAddTask,
        getEditTask: urlsPubsub.getEditTask,
        getTasksTable: urlsPubsub.getTasksTable,
        saveTask: urlsPubsub.saveTask
    },

    init: function () {
        tasksPubsub.showAddTask();
        tasksPubsub.add_init();
        tasksPubsub.edit_init();
    },

    applyBindings: function () {

        $(document).on('click', '#Add_HasTime', this.add_updateHasTime);
        $(document).on('click', '#Edit_HasTime', this.edit_updateHasTime);

        $(document).on('click', '#saveProject', this.saveTask);
        $(document).on('click', '#aside-tab-Add', this.getAddTask);
        $(document).on('click', '#aside-tab-Edit', this.getEditTask);

        $(document).on('click', '#taskResultsTableBody tr', this.selectTask);

        //Tabs
        $(document).on('click', '#aside-tabs-add', tasksPubsub.showAddAside);
        $(document).on('click', '#aside-tabs-edit', tasksPubsub.showEditAside);
    },


    /*
     *  General
     */

    getAddTask: function() {
        $.post(
            tasksPubsub.urls.getAddTask,
            function(resultHtml) {
                $('#aside-content').html(resultHtml);
            });
    },

    getEditTask: function () {

        var wasVisible= $('#aside-edit-container').is(':visible');
        var $editTab = $('#aside-tab-Edit').parent();

        if (!$editTab.hasClass('disabled')) {

            var selectedId = $('#taskResultsTable table').find('.selected').data('taskid');
            
            $.ajax({
                url: tasksPubsub.urls.getEditTask,
                type: 'POST',
                data: { taskId: selectedId },
                success: function(resultHtml) {
                    $('#aside-content').html(resultHtml);
                    tasksPubsub.init();
                }
            });

            if (!wasVisible) {
                tasksPubsub.showAddTask();
            }
        };
    },

    getTasksTable: function () {

        $.post(
            tasksPubsub.urls.getTasksTable,
            function (resultHtml) {
                $('#taskResultsTable').html(resultHtml);
                tasksPubsub.init();
            });
    },

    saveTask: function (e) {

        e.preventDefault();
        var viewModel = {
            Description: $('#Description').val(),
            PriorityId: $('#PriorityId').val(),
            TimeFrameId: $('#TimeFrameId').val(),
            Date: $('#Date').val(),
            HasTime: $('#HasTime').val(),
            Time: $('#Time').val()
        }
        $.ajax({
            url: tasksPubsub.urls.saveTask,
            type: 'POST',
            data: viewModel,
            success: function (result) {
                if (result == "True" || result === true) {
                    tasksPubsub.getTasksTable();
                    layoutpubsub.toggleAside();
                }
            }
        });
    },

    selectTask: function (e) {

        var $row = $(e.target);
        if (!$row.closest('tr').hasClass('selected')) {
            $('#taskResultsTableBody .selected').removeClass('selected');
            $row.closest('tr').addClass('selected');
            tasksPubsub.getEditTask();
        }

        //enable edit tab
        var $editTab = $('#aside-tab-Edit').parent();
        if ($editTab.hasClass('disabled')) {
            $editTab.removeClass('disabled');
        }
    },

    showAddTask: function() {
        $('#aside-edit-container').hide();
        $('#aside-add-container').show();
    },

    showEditTask: function() {
        $('#aside-add-container').hide();
        $('#aside-edit-container').show();
    },

    /*
     *  Add Aside
     */

    add_init: function () {
        //TimeFrame tabs
        $('#add-tabs').tabs({
            activate: function (event, ui) {
                var timeFrameId = 0;
                switch (ui.newPanel.selector) {
                case ('#add-day'):
                    if ($('#HasTime').is(':checked')) {
                        timeFrameId = 0;
                    } else {
                        timeFrameId = 1;
                    }
                    break;
                case ('#add-week'):
                    timeFrameId = 2;
                    break;
                case ('#add-month'):
                    timeFrameId = 3;
                    break;
                case ('#add-open'):
                    timeFrameId = 4;
                    break;
                }
                $('#Add_TimeFrameId').val(timeFrameId);
            }
        });

        //Daypicker customised in order to have the displayed date different to the held date
        tasksPubsub.add_dayPicker();

        //Uses timedropper plug in
        $("#Add_Time").timeDropper({
            backgroundColor: '#ffffff',
            borderColor: '#a9a9a9',
            primaryColor: '#00D8CA'
        });

        //Only shows months when choosing date
        $('#add-month .timeframe-month').MonthPicker({
            Button: false,
            OnAfterChooseMonth: function () {
                $('#Add_Date').val(moment($(this).MonthPicker('GetSelectedDate')).format());
            }
        });

        //Set disabled on button
        tasksPubsub.add_updateHasTime();
    },

    add_updateHasTime: function () {

        if ($('#Add_HasTime').is(':checked')) {
            $('#Add_Time')
                .prop('disabled', false)
                .prop('placeholder', 'Choose time');
        } else {
            $('#Add_Time')
                .val('')
                .prop('placeholder', 'No specified time')
                .prop('disabled', true);
        }
    },

    add_dayPicker: function () {

        var $dayPicker = $("#aside-add-container .timeframe-date");
        var $date = $('#Add_Date');
        var $selectedDate;

        //Standard datepicker
        $dayPicker.datepicker({
            changeMonth: true,
            changeYear: true,
            firstDay: 1,
            beforeShowDay: function (date) {
                var cssClass = 'date-picker';
                if (moment(date).isSame($selectedDate)) {
                    cssClass = 'date-picker selected';
                }
                return [true, cssClass];
            },
            onSelect: function (dateText, inst) {

                $selectedDate = moment(dateText, 'MM-DD-YYYY');//TODO Fix this. on select not working and saving select
                $date.val($selectedDate.format());

                $dayPicker.val($selectedDate.calendar(null,
                    {
                        sameDay: '[Today]',
                        nextDay: '[Tomorrow]',
                        nextWeek: 'dddd',
                        lastDay: '[Yesterday]',
                        lastWeek: '[Last] dddd',
                        sameElse: 'Do MMM YYYY'
                    }));
                console.log('after: ' + $selectedDate.format());
                tasksPubsub.updateHasTime();
            }
        }
        );

        $(document).on('mousemove',
            '#ui-datepicker-div .date-picker',
            function () {
                $(this).closest('td').addClass('hover');
            });

        $(document).on('mouseleave',
            '#ui-datepicker-div .date-picker',
            function () {
                $(this).closest('td').removeClass('hover');
            });
    },

    add_weekPicker: function () {

        var $weekPicker = $('#aside-add-container .timeframe-week');
        var $date = $('#Add_Date');
        var $startDate = moment(moment($date).year()).add(moment($date).week() - 1, 'weeks');
        var $endDate = moment($startDate).add(6, 'days');

        $weekPicker.datepicker({
            beforeShowDay: function (date) {

                var cssClass = 'week-picker';
                if (moment(date).isBetween(
                    moment($startDate).subtract(1, 'day'),
                    moment($endDate).add(1, 'day'),
                    'day')) //isBetween is exclusive
                {
                    cssClass = 'week-picker selected';
                }
                return [true, cssClass];
            },
            firstDay: 1,
            onClose: function () {

                console.log($weekPicker.datepicker('getDate'));
                $date.val(moment($startDate).format());

                var startDateFormat = 'Do';
                var endDateFormat = 'Do MMM YY';

                if ($startDate.month() !== $endDate.month()) {
                    startDateFormat = startDateFormat + ' MMM';
                }
                if ($startDate.year() !== $endDate.year()) {
                    startDateFormat = startDateFormat + ' YY';
                }
                startDateFormat = startDateFormat + ' - ';

                $weekPicker.val(
                    'W' + moment($startDate).week() + ' (' +
                    $startDate.format(startDateFormat) +
                    $endDate.format(endDateFormat) + ')');
            },
            onSelect: function (dateText, inst) {

                $startDate = moment(dateText, 'MM-DD-YYYY').startOf('isoWeek');
                $endDate = moment($startDate).endOf('isoWeek');
                $date.val(moment($startDate).week());
            },
            selectOtherMonths: true,
            showOtherMonths: true,
            showWeek: true
        });

        $(document).on('mousemove',
            '#ui-datepicker-div .week-picker',
            function () {
                $(this).closest('tr').find('td').addClass('hover');
            });

        $(document).on('mouseleave',
            '#ui-datepicker-div .week-picker',
            function () {
                $(this).closest('tr').find('td').removeClass('hover');
            });
    },


    /*
     *  Edit Aside
     */

    edit_init: function () {
        //TimeFrame tabs
        $('#edit-tabs').tabs({
            activate: function (event, ui) {
                var timeFrameId = 0;
                switch (ui.newPanel.selector) {
                    case ('#edit-day'):
                    if ($('#Edit_HasTime').is(':checked')) {
                        timeFrameId = 0;
                    } else {
                        timeFrameId = 1;
                    }
                    break;
                case ('#edit-week'):
                    timeFrameId = 2;
                    break;
                case ('#edit-month'):
                    timeFrameId = 3;
                    break;
                case ('#edit-open'):
                    timeFrameId = 4;
                    break;
                }
                $('#TimeFrameId').val(timeFrameId);
            }
        });

        $("#Edit_Time").timeDropper({
            backgroundColor: '#ffffff',
            borderColor: '#a9a9a9',
            primaryColor: '#00D8CA'
        });

        //Only shows months when choosing date
        $('#edit-month .timeframe-month').MonthPicker({
            Button: false,
            OnAfterChooseMonth: function () {
                $('#Edit_Date').val(moment($(this).MonthPicker('GetSelectedDate')).format());
            }
        });
    },

    edit_updateHasTime: function () {

        if ($('#Edit_HasTime').is(':checked')) {
            $('#Edit_Time')
                .prop('disabled', false)
                .prop('placeholder', 'Choose time');
        } else {
            $('#Edit_Time')
                .val('')
                .prop('placeholder', 'No specified time')
                .prop('disabled', true);
        }
    },

    edit_dayPicker: function () {

        var $dayPicker = $("#aside-edit-container .timeframe-date");
        var $date = $('#Edit_Date');
        var $selectedDate;

        //Standard datepicker
        $dayPicker.datepicker({
            changeMonth: true,
            changeYear: true,
            firstDay: 1,
            beforeShowDay: function (date) {
                var cssClass = 'date-picker';
                if (moment(date).isSame($selectedDate)) {
                    cssClass = 'date-picker selected';
                }
                return [true, cssClass];
            },
            onSelect: function (dateText, inst) {

                $selectedDate = moment(dateText, 'MM-DD-YYYY');//TODO Fix this. on select not working and saving select
                $date.val($selectedDate.format());

                $dayPicker.val($selectedDate.calendar(null,
                    {
                        sameDay: '[Today]',
                        nextDay: '[Tomorrow]',
                        nextWeek: 'dddd',
                        lastDay: '[Yesterday]',
                        lastWeek: '[Last] dddd',
                        sameElse: 'Do MMM YYYY'
                    }));
                console.log('after: ' + $selectedDate.format());
                tasksPubsub.updateHasTime();
            }
        }
        );

        $(document).on('mousemove',
            '#ui-datepicker-div .date-picker',
            function () {
                $(this).closest('td').addClass('hover');
            });

        $(document).on('mouseleave',
            '#ui-datepicker-div .date-picker',
            function () {
                $(this).closest('td').removeClass('hover');
            });
    },

    edit_weekPicker: function () {

        var $weekPicker = $('#aside-edit-container .timeframe-week');
        var $date = $('#Edit_Date');
        var $startDate = moment(moment($date).year()).add(moment($date).week() - 1, 'weeks');
        var $endDate = moment($startDate).add(6, 'days');

        $weekPicker.datepicker({
            beforeShowDay: function (date) {

                var cssClass = 'week-picker';
                if (moment(date).isBetween(
                    moment($startDate).subtract(1, 'day'),
                    moment($endDate).add(1, 'day'),
                    'day')) //isBetween is exclusive
                {
                    cssClass = 'week-picker selected';
                }
                return [true, cssClass];
            },
            firstDay: 1,
            onClose: function () {

                console.log($weekPicker.datepicker('getDate'));
                $date.val(moment($startDate).format());

                var startDateFormat = 'Do';
                var endDateFormat = 'Do MMM YY';

                if ($startDate.month() !== $endDate.month()) {
                    startDateFormat = startDateFormat + ' MMM';
                }
                if ($startDate.year() !== $endDate.year()) {
                    startDateFormat = startDateFormat + ' YY';
                }
                startDateFormat = startDateFormat + ' - ';

                $weekPicker.val(
                    'W' + moment($startDate).week() + ' (' +
                    $startDate.format(startDateFormat) +
                    $endDate.format(endDateFormat) + ')');
            },
            onSelect: function (dateText, inst) {

                $startDate = moment(dateText, 'MM-DD-YYYY').startOf('isoWeek');
                $endDate = moment($startDate).endOf('isoWeek');
                $date.val(moment($startDate).week());
            },
            selectOtherMonths: true,
            showOtherMonths: true,
            showWeek: true
        });

        $(document).on('mousemove',
            '#ui-datepicker-div .week-picker',
            function () {
                $(this).closest('tr').find('td').addClass('hover');
            });

        $(document).on('mouseleave',
            '#ui-datepicker-div .week-picker',
            function () {
                $(this).closest('tr').find('td').removeClass('hover');
            });
    }
}