/// <reference path="../../framework/jquery-ui-1.12.1.js" />
/// <reference path="../../framework/jquery-1.10.2.intellisense.js" />
/// <reference path="../../framework/timedropper.js" />
/// <reference path="../../framework/monthpicker.js" />

$(function() {
    tasksPubsub.init();
    tasksPubsub.applyBindings();
});

var tasksPubsub = {

    $timeFrameId:$(),
    getThougtId: function (e) { return $(e.target).closest('tr')[0].dataset.thoughtid; },

    urls: {
        GetAddTaskPopup: Core.ResolveUrl('GetAddTask', 'Tasks'),
        GetTasksTable: Core.ResolveUrl('GetTasksTable', 'Tasks'),
        SaveTask: Core.ResolveUrl('Create', 'Tasks')
    },

    applyBindings: function() {
        $(document).on('click', '#timeframe-tabs .ui-id-1', this.showTimeframeTab('day'));
        $(document).on('click', '#timeframe-tabs .ui-id-2', this.showTimeframeTab('week'));
        $(document).on('click', '#timeframe-tabs .ui-id-3', this.showTimeframeTab('month'));
        $(document).on('click', '#timeframe-tabs .ui-id-4', this.showTimeframeTab('open'));
    },

    init: function() {
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

        //Standard datepicker
        $("#timeframe-date").datepicker({
                changeMonth: true,
                changeYear: true
            }
        );

        //Uses timedropper plug in
        $("#timeframe-time").timeDropper({
            primaryColor: '#00D8CA',
            backgroundColor: '#ffffff',
            borderColor: '#a9a9a9'
        });

        //Only shows months when choosing date
        $('#timeframe-month').MonthPicker({
            Button: false
        });

        //Week picker is custom made jQuery datePicker
        tasksPubsub.initWeekPicker();
    },

    initWeekPicker: function() {

        var $weekPicker = $('#timeframe-week');
        var startDate = '';
        var endDate = '';
        var selectedDate = '';


        $('#timeframe-week').datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            onSelect: function(dateText, inst) {
                selectedDate = $(this).datepicker('getDate');
                startDate = window.moment(selectedDate).startOf('isoWeek').format();
                endDate = window.moment(selectedDate).endOf('isoWeek').format();
            },
            firstDay: 1,
            showWeek: true,
            beforeShow: function(input, inst) {
                if (selectedDate === '') {
                    selectedDate = new Date($('#timeframe-week').val());
                    startDate = moment(selectedDate).startOf('isoWeek').format(); //IsoWeek sets week start to Monday
                    endDate = moment(selectedDate).endOf('isoWeek').format();
                }
            },
            beforeShowDay: function(date) {

                var cssClass = 'week-picker';
                if (moment(date).isBetween(
                    moment(startDate).subtract(1, 'day'),
                    moment(endDate).add(1, 'day'),
                    'day')) //isBetween is exclusive
                {
                    cssClass = 'week-picker selected';
                }
                return [true, cssClass];
            }
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

    getTasksTable: function() {
        $.post(tasksPubsub.urls.GetTasksTable,
            function(resultHtml) {
                $('#taskResultsTable').html(resultHtml);
                tasksPubsub.init();
            });
    },

    saveTask: function() {
        e.preventDefault();
        var viewModel = $('#addTaskForm').serialize();
        $.post(tasksPubsub.urls.SaveThought,
            viewModel,
            function(result) {
                if (result == "True" || result === true) {
                    tasksPubsub.getThoughtsTable();
                }
            });
    },

    selectCurrentWeek: function() {
        window.setTimeout(function() {
                $('#timeframe-week').find('.ui-datepicker-current-day a').addClass('ui-state-active');
            },
            1);
    },

    updateTimeFrameId: function() {

    },

    updateAddTaskModel: {
        description: $('#timeframe-date').val(),
        priorityId: $('#timeframe-date').val(),
        timeFrameId: $('#timeframe-date').val(),
        Date: $('#timeframe-date').val(),
        HasTime: $('#timeframe-date').val(),
        time: $('#timeframe-time').val(),
        weekNumber: $('#timeframe-date').val(),
        month: $('#timeframe-date').val()
    }
}