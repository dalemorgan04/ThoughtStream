(function ($) {
    $.fn.timeFrame = function(options) {

        var
            $container = $(this),
            opt = $.extend({
                timeFrameId: 0,
                date: '',
                time: ''
            }, options),
            html =
            '<div class="timeframe-container">' +

                /*Tab buttons*/
                '<ul>' +
                    '<li class = "button-open selected"><a> Anytime </a></li>' +
                    '<li class="button-day" > <a> Day </a> </li>' +
                    '<li class="button-week" > <a> Week </a> </li>' +
                    '<li class="button-month" > <a> Month </a> </li>' +
                '</ul>' + 

                /*Timeframe Output*/
                '<input type="hidden" class="timeframe-output-type" />' +
                '<input type="hidden" class="timeframe-output-date" />' +
                '<input type="hidden" class="timeframe-output-time" />' +

                /*Open*/
                '<div class="tab-open" style="display: none;">' +
                    '<h3>No due date</h3>' +
                '</div>' +

                /*Day*/
                '<div class="tab-day" style="display: none;">' +

                    /*Date*/
                    '<h3>Due on the day of...</h3>' +
                    '<div class="input-group">' +
                        '<div class="input-container-text fit">' +
                            '<input class="timeframe-date" placeholder="Click to choose date" readonly="readonly"/>' +
                            '<span class="underline"></span>' +
                        '</div>' + 
                    '</div>' + 

                    /*Time*/
                    '<h3>At the time...</h3>' +
                    '<div class="input-group">' +
                        
                        '<label class="check fit pad-right">' +
                            '<input type="checkbox" class="timeframe-time-checkbox"/>' +
                            '<div class="box"></div>' +
                        '</label>' +
                        
                        '<div class="input-container-text">' +
                            '<input class="timeframe-time" placeholder="Check box to choose time" readonly="readonly" disabled/>' +
                            '<span class="underline"></span>' +
                        '</div>' + 
                        
                    '</div>' +

                '</div>' +

                /*Week*/
                '<div class="tab-week" style="display: none;">' +

                    '<h3>Due in the week of...</h3>' +

                    '<div class="input-group fit">' +
                        '<div class="input-container-text">' +
                            '<input class="timeframe-week" placeholder="Click to choose a week" readonly="readonly"/>' +
                            '<span class="underline"></span>' +
                        '</div>' + 
                    '</div>' + 

                '</div>' +

                /*Month*/
                '<div class="tab-month" style="display: none;">' +
                    '<h3>Due in the month of...</h3>' +

                    '<div class="input-group fit">' +
                        '<div class="input-container-text">' +
                            '<input class="timeframe-month" placeholder="Click to choose a month" readonly="readonly"/>' +
                            '<span class="underline"></span>' +
                        '</div>' +
                    '</div>' +

                '</div>' +

            '</div>';
        this.html(html);

        //Instantiate $ objects
        var
            $openTab = $container.find('.tab-open'),
            $openButton = $container.find('.button-open'),
            $dayTab = $container.find('.tab-day'),
            $dayButton = $container.find('.button-day'),
            $weekTab = $container.find('.tab-week'),
            $weekButton = $container.find('.button-week'),
            $monthTab = $container.find('.tab-month'),
            $monthButton = $container.find('.button-month'),
            $currentTab ='',
            $outputTypeId = $container.find('.timeframe-output-type'),
            $outputDate = $container.find('.timeframe-output-date'),
            $outputTime = $container.find('.timeframe-output-time'),
            pickerZIndex = $(this).css('z-index') + 1;

        //Set values from options
        $outputTypeId.val(opt.timeFrameId);
        $outputDate.val(opt.date);
        $outputTime.val(opt.time);

        //Init date pickers

        /*Date Picker*/
        var initDayPicker = function() {

            var
                $dayPicker = $dayTab.find('.timeframe-date'),
                MselectedDate = moment($outputDate.val(), "DD/MM/YYYY");

            $dayPicker.datepicker({
                    zIndex: pickerZIndex,
                    changeMonth: true,
                    changeYear: true,
                    firstDay: 1,
                    beforeShowDay: function(date) {
                        var cssClass = 'date-picker';
                        if (moment(date).isSame(MselectedDate)) {
                            cssClass = 'date-picker selected';
                        }
                        return [true, cssClass];
                    },
                    onSelect: function(dateText, inst) {
                        MselectedDate = moment(dateText, 'MM-DD-YYYY'); 
                        $outputDate.val(MselectedDate.format("DD/MM/YYYY"));
                        $dayPicker.val(MselectedDate.calendar(null,
                            {
                                sameDay: '[Today]',
                                nextDay: '[Tomorrow]',
                                nextWeek: 'dddd',
                                lastDay: '[Yesterday]',
                                lastWeek: '[Last] dddd',
                                sameElse: 'Do MMM YYYY'
                            }));
                    }
                }
            );

            $(document).on('mousemove',
                '#ui-datepicker-div .date-picker',
                function() {
                    $(this).closest('td').addClass('hover');
                });

            $(document).on('mouseleave',
                '#ui-datepicker-div .date-picker',
                function() {
                    $(this).closest('td').removeClass('hover');
                });

            if (opt.timeFrameId === "1" || opt.timeFrameId === "2") {
                $dayPicker.val(
                    MselectedDate.calendar(null,
                    {
                        sameDay: '[Today]',
                        nextDay: '[Tomorrow]',
                        nextWeek: 'dddd',
                        lastDay: '[Yesterday]',
                        lastWeek: '[Last] dddd',
                        sameElse: 'Do MMM YYYY'
                    }));
            }
        };
        initDayPicker();

        /*Time Picker*/
        var updateOutputTime = function () {
            $outputTime.val(
                moment(
                    $dayTab.find('.timeframe-time').val() , 'H:mm a'
                ).format("HH:mm")
            );
        };
        var initTimePicker = function() {

            var $time = $dayTab.find('.timeframe-time');

            $time
                .timeDropper({
                    backgroundColor: '#ffffff',
                    borderColor: '#a9a9a9',
                    primaryColor: '#00D8CA',
                    focusClass: 'focus',
                    focusOut: updateOutputTime
                });
                
            if (opt.timeFrameId === "1") {
                $dayTab.find('.timeframe-time-checkbox').prop('checked', true);
                $time
                    .prop('disabled', false)
                    .val(moment($outputTime.val(), "HH:mm").format("h:mma"));
                $outputTime.val(opt.time);
            } else {
                $dayTab.find('.timeframe-time-checkbox').prop('checked', false);
                $time
                    .prop('disabled', true)
                    .val('');
                $outputTime.val('');
            }
            
        };
        initTimePicker();

        /*Week Picker*/
        var initWeekPicker = function() {

            var
                $weekPicker = $weekTab.find('.timeframe-week'),
                MstartDate = moment($outputDate.val(), "DD/MM/YYYY").day("Monday"),
                MendDate = moment(MstartDate, "DD/MM/YYYY").add(6, 'days');

            $weekPicker.datepicker({
                beforeShowDay: function(date) {

                    var cssClass = 'week-picker';
                    if (moment(date).isBetween(
                        moment(MstartDate).subtract(1, 'day'),
                        moment(MendDate).add(1, 'day'),
                        'day')) //isBetween is exclusive
                    {
                        cssClass = 'week-picker selected';
                    }
                    return [true, cssClass];
                },
                firstDay: 1,
                onClose: function() {

                    console.log($weekPicker.datepicker('getDate'));
                    $outputDate.val(MstartDate.format("DD/MM/YYYY"));

                    var startDateFormat = 'Do';
                    var endDateFormat = 'Do MMM YY';

                    if (MstartDate.month() !== MendDate.month()) {
                        startDateFormat = startDateFormat + ' MMM';
                    }
                    if (MstartDate.year() !== MendDate.year()) {
                        startDateFormat = startDateFormat + ' YY';
                    }
                    startDateFormat = startDateFormat + ' - ';

                    $weekPicker.val(
                        'W' +
                        MstartDate.format('W') +
                        ' (' +
                        MstartDate.format(startDateFormat) +
                        MendDate.format(endDateFormat) +
                        ')');
                },
                onSelect: function(dateText, inst) {

                    MstartDate = moment(dateText, 'MM-DD-YYYY').startOf('isoWeek');
                    MendDate = moment(MstartDate).endOf('isoWeek');
                    $outputDate.val(moment(MstartDate).week());
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

            if (opt.timeFrameId === "3") {
                //First time opening
                var startDateFormat = 'Do';
                var endDateFormat = 'Do MMM YY';

                if (MstartDate.month() !== MendDate.month()) {
                    startDateFormat = startDateFormat + ' MMM';
                }
                if (MstartDate.year() !== MendDate.year()) {
                    startDateFormat = startDateFormat + ' YY';
                }
                startDateFormat = startDateFormat + ' - ';

                $weekPicker.val(
                    'W' +
                    MstartDate.format('W') +
                    ' (' +
                    MstartDate.format(startDateFormat) +
                    MendDate.format(endDateFormat) +
                    ')');
            }
            
        };
        initWeekPicker();

        /*Month Picker*/
        var initMonthPicker = function() {

            var $monthPicker = $monthTab.find('.timeframe-month');

            $monthPicker.MonthPicker({
                Button: false,
                OnAfterChooseMonth: function() {
                    $outputDate.val(
                        moment($(this).MonthPicker('GetSelectedDate')).format('DD/MM/YYYY')
                    );
                    $(this).val(
                        moment($(this).MonthPicker('GetSelectedDate')).format('MMMM YYYY')
                    );
                }
            });

            if (opt.timeFrameId === "4") {
                $monthPicker.val(moment($outputDate.val(), "DD/MM/YYYY").format('MMMM YYYY'));    
            }
            
        };
        initMonthPicker();

        //Bind buttons
        var openTab = function(tabName) {

            var
                $tabToOpen,
                $tabButton,
                timeFrameTypeId;

            switch (tabName) {
            case 'open':
                $tabToOpen = $openTab;
                $tabButton = $openButton;
                timeFrameTypeId = 0;
                break;
            case 'day':
                $tabToOpen = $dayTab;
                $tabButton = $dayButton;
                /*Check if time has been set*/
                if ($dayTab.find('.timeframe-time-checkbox').is(':checked')) {
                    timeFrameTypeId = 1;
                } else {
                    timeFrameTypeId = 2;
                }
                break;
            case 'week':
                $tabToOpen = $weekTab;
                $tabButton = $weekButton;
                timeFrameTypeId = 3;
                break;
            case 'month':
                $tabToOpen = $monthTab;
                $tabButton = $monthButton;
                timeFrameTypeId = 4;
                break;
            default:
                $tabToOpen = $openTab;
                $tabButton = $openButton;
                timeFrameTypeId = 0;
                break;
            }
            
            if ($currentTab === '') {
                //First time opening
                $tabToOpen.velocity('transition.slideLeftIn', 300);
                $currentTab = $tabToOpen;
                $tabButton.closest('ul').find('.selected').removeClass('selected');
                $tabButton.addClass('selected');
            } else
                if (!$tabToOpen.is($currentTab)) {
                    $currentTab.velocity('transition.slideRightOut',
                        {
                            duration: 200,
                            complete: function() {
                                $tabToOpen.velocity('transition.slideLeftIn', 300);
                                $currentTab = $tabToOpen;
                                $tabButton.closest('ul').find('.selected').removeClass('selected');
                                $tabButton.addClass('selected');
                                $outputTypeId.val(timeFrameTypeId);
                            }
                        });
            } 
        };

        var toggleTime = function() {

            var
                isVisible = this.checked,
                $time = $dayTab.find('.timeframe-time');

            if (isVisible) {
                $outputTypeId.val(1);
                $time
                    .prop('disabled', false)
                    .val(moment().format('h:mma'))
                    .click()
                    .focus();
            } else {
                $outputTypeId.val(2);
                $time
                    .prop('disabled', true)
                    .val('');
                $outputTime.val('');
            }
        };
        
        $openButton.click(function () { openTab('open'); });
        $dayButton.click(function () { openTab('day'); });
        $weekButton.click(function () { openTab('week'); });
        $monthButton.click(function () { openTab('month'); });
        
        $dayTab.find('.timeframe-time').click(function() {
            $(this).next('.underline').addClass('focus');
        });

        $dayTab.find('.timeframe-time-checkbox').change(toggleTime);
        $dayTab.find('.timeframe-time').change(updateOutputTime);

        //Open timeframe tab
        switch (opt.timeFrameId) {
            case "1": //Time
                openTab('day');
                break;
            case "2": //Date
                openTab('day');
                break;
            case "3": //Week
                openTab('week');
                break;
            case "4": //Month
                openTab('month');
                break;
            default: //case 0 open
                openTab('open');
                break;
        }
    };
}(jQuery));