(function ($) {
    $.fn.timeFrame = function(options) {

        var
            $container = $(this),
            opt = $.extend({
                timeFrameId: 0,
                date: '',
                time: ''
            }),
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
                '<div class="tab-open">' +
                    '<h3>No due date</h3>' +
                '</div>' +

                /*Day*/
                '<div class="tab-day">' +

                    /*Date*/
                    '<h3>Due on the date of...</h3>' +
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
                '<div class="tab-week">' +

                    '<h3>Due in the week of...</h3>' +

                    '<div class="input-group fit">' +
                        '<div class="input-container-text">' +
                            '<input class="timeframe-week" placeholder="Click to choose a week" readonly="readonly"/>' +
                            '<span class="underline"></span>' +
                        '</div>' + 
                    '</div>' + 

                '</div>' +

                /*Month*/
                '<div class="tab-month">' +
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

        var
            $openTab = $container.find('.tab-open'),
            $openButton = $container.find('.button-open'),
            $dayTab = $container.find('.tab-day'),
            $dayButton = $container.find('.button-day'),
            $weekTab = $container.find('.tab-week'),
            $weekButton = $container.find('.button-week'),
            $monthTab = $container.find('.tab-month'),
            $monthButton = $container.find('.button-month'),
            $currentTab = $openTab,
            $outputTypeId = $container.find('.timeframe-output-type'),
            $outputDate = $container.find('.timeframe-output-date'),
            $outputTime = $container.find('.timeframe-output-time'),
            pickerZIndex = $(this).css('z-index') + 1 ;

        /*Date Picker*/
        var initDayPicker = function () {

            var
                $dayPicker = $dayTab.find('.timeframe-date'),
                $selectedDate;
            
            $dayPicker.datepicker({
                    zIndex: pickerZIndex,
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
                        $outputDate.val($selectedDate.format());

                        $dayPicker.val($selectedDate.calendar(null,
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
                function () {
                    $(this).closest('td').addClass('hover');
                });

            $(document).on('mouseleave',
                '#ui-datepicker-div .date-picker',
                function () {
                    $(this).closest('td').removeClass('hover');
                });
        }
        initDayPicker();

        /*Time Picker*/
        var initTimePicker = function () {

            var $time = $dayTab.find('.timeframe-time');

            $time
                .timeDropper({
                    backgroundColor: '#ffffff',
                    borderColor: '#a9a9a9',
                    primaryColor: '#00D8CA',
                    focusClass: 'focus'
                })
                .val('');
        }
        initTimePicker();

        /*Week Picker*/
        var initWeekPicker = function () {

            var
                $weekPicker = $weekTab.find('.timeframe-week'),
                $startDate = moment(moment($outputDate).year()).add(moment($outputDate).week() - 1, 'weeks'),
                $endDate = moment($startDate).add(6, 'days');

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
                    $outputDate.val(moment($startDate).format());

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
                        'W' + moment($startDate).format('W') + ' (' +
                        $startDate.format(startDateFormat) +
                        $endDate.format(endDateFormat) + ')');
                },
                onSelect: function (dateText, inst) {

                    $startDate = moment(dateText, 'MM-DD-YYYY').startOf('isoWeek');
                    $endDate = moment($startDate).endOf('isoWeek');
                    $outputDate.val(moment($startDate).week());
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
        initWeekPicker();

        /*Month Picker*/
        var initMonthPicker = function () {

            var $monthPicker = $monthTab.find('.timeframe-month');

            $monthPicker.MonthPicker({
                Button: false,
                OnAfterChooseMonth: function () {
                    $outputDate.val(
                        moment($(this).MonthPicker('GetSelectedDate')).format('DD/MM/YYYY')
                    );
                    $(this).val(
                        moment($(this).MonthPicker('GetSelectedDate')).format('MMMM YYYY')
                    );
                }
            });
        }
        initMonthPicker();

        /*Bindings*/

        var openTab = function (tabName) {

            var
                $tabToOpen,
                $tabButton,
                timeFrameTypeId;

            switch (tabName) {
                case 'open':
                    $tabToOpen = $openTab;
                    $tabButton = $openButton;
                    timeFrameTypeId = 0 ;
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
        }

        var toggleTime = function () {

            var
                isVisible = this.checked,
                $time = $dayTab.find('.timeframe-time');

            if (isVisible) {
                $outputTypeId.val(1);
                $time
                    .prop('disabled', false)
                    .val(moment().format('h:mm a'))
                    .click()
                    .focus();
                $outputTime.val($time.val());
            } else {
                $outputTypeId.val(2);
                $time
                    .prop('disabled', true)
                    .val('');
                $outputTime.val('');
            }
        }
        
        $openButton .click( function () { openTab('open'  ); });
        $dayButton  .click( function () { openTab('day'   ); });
        $weekButton .click( function () { openTab('week'  ); });
        $monthButton.click(function () { openTab('month'); });
        
        $dayTab.find('.timeframe-time').click(function() {
            $(this).next('.underline').addClass('focus');
        });

        $dayTab.find('.timeframe-time-checkbox').change(toggleTime);

        //Set init state
        $openTab.css('display', 'none');
        $dayTab.css('display', 'none');
        $weekTab.css('display', 'none');
        $monthTab.css('display', 'none');
        $outputTime.val('');
        switch (opt.timeFrameId) {
            case 0: //Open
                $outputTypeId.val(0);
                $outputDate.val('');
                $openTab.css('display', '');
                break;
            case 1: //Time
                $outputTypeId.val(1);
                $outputDate.val(opt.date);
                $outputTime.val(opt.time);
                $dayTab.css('display', '');
                break;
            case 2: //Date
                $outputTypeId.val(2);
                $outputDate.val(opt.date);
                $dayTab.css('display', '');
                break;
            case 3: //Week
                $outputTypeId.val(3);
                $outputDate.val(opt.date);
                $weekTab.css('display', '');
                break;
            case 4: //Month
                $outputTypeId.val(4);
                $outputDate.val(opt.date);
                $monthTab.css('display', '');
                break;
        default:
        }
    };
}(jQuery));