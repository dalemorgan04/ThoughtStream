/// <reference path="../../framework/jquery-ui-1.12.1.js" />
/// <reference path="../../framework/jquery-1.10.2.intellisense.js" />
/// <reference path="../../framework/timedropper.js" />

$(function () {
    tasksPubsub.init();
    tasksPubsub.applyBindings();
});

var tasksPubsub = {

    getThougtId: function(e) { return $(e.target).closest('tr')[0].dataset.thoughtid; },
    urls: {
        GetAddTaskPopup: Core.ResolveUrl('GetAddTask', 'Tasks'),
        GetTasksTable: Core.ResolveUrl('GetTasksTable', 'Tasks'),
        SaveTask: Core.ResolveUrl('Create', 'Tasks')
    },

    init: function() {
        //Standard datepicker
        $("#timeframe-date").datepicker({
            changeMonth: true,
            changeYear: true
            }
        );

        //Uses timedropper plug in
        $("#timeframe-time").timeDropper({
            primaryColor: '#00D8CA',
            backgroundColor: '#f7f7f7',
            borderColor: '#626466'
        });
        //Only shows months when choosing date
        $('#timeframe-month').datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'MM yy',
            beforeShow: function(input, inst) {
                $('#ui-datepicker-div').addClass('month-picker');
            },
            onClose: function (dateText, inst) {
                $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
                $('#ui-datepicker-div').removeClass('month-picker');
            }
        });

        //Chooses a week, custom built version of datepicker
        tasksPubsub.weekPicker();
    },

    weekPicker: function() {

        var startDate = '';
        var endDate = '';
        var date = '';

        $('#timeframe-week').datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            onSelect: function(dateText, inst) {

                date = $(this).datepicker('getDate');
                startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay());
                endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 6);

                var dateFormat = inst.settings.dateFormat || $.datepicker._defaults.dateFormat;
                $('#startDate').text($.datepicker.formatDate(dateFormat, startDate, inst.settings));
                $('#endDate').text($.datepicker.formatDate(dateFormat, endDate, inst.settings));
            },
            showWeek: true,
            firstday: 1, 
            beforeShow: function(input, inst) {
                if (date === '') {
                    date = new Date($('#timeframe-week').val());
                    startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay());
                    endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 6);
                } 
            },

            beforeShowDay: function (date) {
                
                var cssClass = 'week-picker';
                if (date >= startDate && date <= endDate){
                    cssClass = 'week-picker selected';
                } 
                return [true, cssClass];
            },
            
            onChangeMonthYear: function (year, month, inst) {
                selectCurrentWeek();
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

    monthPicker: function() {
        $('#timeframe-month').datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            dateFormat: 'MM yy',
            onClose: function (dateText, inst) {
                $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
            }
        });
    },

    applyBindings: function() {
        $(document).on('click', '#btnShowAddTask', this.ShowAddTaskPopup);
        $(document).on('click', '#btnAddTask', this.saveTask);
        $(document).on('click', '', this.updateTimeFrameId);
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

    showDayTimeFrame: function() {
       
    },

    showWeekTimeFrame: function() {
        
    },

    showMonthTimeFrame: function() {
        
    }
}