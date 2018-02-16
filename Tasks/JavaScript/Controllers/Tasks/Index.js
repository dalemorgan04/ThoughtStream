/// <reference path="../../framework/jquery-ui-1.12.1.js" />
/// <reference path="../../framework/jquery-1.10.2.intellisense.js" />
/// <reference path="../../framework/timedropper.js" />
/// <reference path="../../framework/monthpicker.js" />
/// <reference path="../../framework/moment.js" />
/// <reference path="../../framework/layout.js" />

$(function () {
    tasksPubsub.init();
    tasksPubsub.applyBindings();
});

var tasksPubsub = {

    $timeFrameId:$(),
    getThougtId: function (e) { return $(e.target).closest('tr')[0].dataset.thoughtid; },

    urls: {

        getAddTask: urlsPubsub.getAddTask,
        getEditTask: urlsPubsub.getEditTask,
        getTasksTable: urlsPubsub.getTasksTable,
        saveTask: urlsPubsub.saveTask
    },

    applyBindings: function () {

        $(document).on('click', '#HasTime', this.updateHasTime);
        $(document).on('click', '#saveTask', this.saveTask);
        $(document).on('click', '#aside-tab-Add', this.getAddTask);
        $(document).on('click', '#aside-tab-Edit', this.getEditTask);
        $(document).on('click', '#taskResultsTableBody tr', this.selectTask);
    },

    init: function () {



        //TimeFrame tabs
        $('#timeframe-tabs').tabs({
            activate: function (event, ui) {
                var timeFrameId = 0;
                switch (ui.newPanel.selector) {
                    case ('#timeframe-day-container'):
                        if ($('#HasTime').is(':checked')) {
                            timeFrameId = 0;
                        } else {
                            timeFrameId = 1;
                        }
                        break;
                    case ('#timeframe-week-container'):
                        timeFrameId = 2;
                        break;
                    case ('#timeframe-month-container'):
                        timeFrameId = 3;
                        break;
                    case ('#timeframe-open-container'):
                        timeFrameId = 4;
                        break;
                }
                $('#TimeFrameId').val(timeFrameId);
            }
        });

        //Daypicker customised in order to have the displayed date different to the held date
        tasksPubsub.dayPicker();

        //Uses timedropper plug in
        $("#Time").timeDropper({
            backgroundColor: '#ffffff',
            borderColor: '#a9a9a9',
            primaryColor: '#00D8CA'
        });

        //Only shows months when choosing date
        $('#timeframe-month').MonthPicker({
            Button: false,
            OnAfterChooseMonth: function () {
                $('#Date').val(moment($(this).MonthPicker('GetSelectedDate')).format());
            }
        });


        //Week picker is custom made jQuery datePicker
        tasksPubsub.weekPicker();

        //Set disabled on button
        tasksPubsub.updateHasTime();
    },

    enableEdit: function() {
        //$('#aside-tab-Edit').
    },

    updateHasTime: function () {

        if ($('#HasTime').is(':checked')) {
            $('#Time')
                .prop('disabled', false)
                .prop('placeholder', 'Choose time');
        } else {
            $('#Time')
                .val('')
                .prop('placeholder','No specified time')
                .prop('disabled', true);
        }
    },

    dayPicker: function () {

        var $dayPicker = $("#timeframe-date");
        var $date = $('#Date');
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

    weekPicker: function () {

        var $weekPicker = $('#timeframe-week');
        var $date = $('#Date');
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
            function() {
                $(this).closest('tr').find('td').addClass('hover');
            });

        $(document).on('mouseleave',
            '#ui-datepicker-div .week-picker',
            function() {
                $(this).closest('tr').find('td').removeClass('hover');
            });
    },

    getTasksTable: function () {

        $.post(
            tasksPubsub.urls.getTasksTable,
            function(resultHtml) {
                $('#taskResultsTable').html(resultHtml);
                tasksPubsub.init();
            });
    },

    getAddTask: function() {
        $.post(
            tasksPubsub.urls.getAddTask,
            function(resultHtml) {
                $('#aside-content').html(resultHtml);
                tasksPubsub.init();
            });
    },

    getEditTask: function() {
        $.post(
            tasksPubsub.urls.getEditTask,
            function (resultHtml) {
                $('#aside-content').html(resultHtml);
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
        $.post(
            tasksPubsub.urls.saveTask,
            viewModel,
            function(result) {
                if (result == "True" || result === true) {
                    tasksPubsub.getTasksTable();
                    layoutpubsub.toggleAside();
                }
            });
    },

    selectTask: function (e) {

        var $row = $(e.target);
        if (!$row.closest('tr').hasClass('selected')) {
            $('#taskResultsTableBody .selected').removeClass('selected');
            $row.closest('tr').addClass('selected');
        }
    }
}