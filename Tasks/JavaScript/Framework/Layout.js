/// <reference path="jquery-1.10.2.js" />
/// <reference path="velocity.min.js" />
/// <reference path="velocity.ui.js" />

/*
*   Developed by Dale Morgan
*   dalemoragn04@gmail.com
*/

$(function () {
    layoutpubsub.init();
    layoutpubsub.applyBindings();

});

var layoutpubsub = {

    // ===== Layout Settings ===== //    
    setNavbarWidth: '150px',
    setAsideWidth: '350px',
    setTransitionTime: 250,
    // =========================== //    

    navbarIsVisible: true,
    navbarWidth: '',
    navArrowAnimationSequence: [],

    asideIsVisible: true,
    asideWidth: '',
    asideArrowAnimationSequence: [],

    init: function () {
        layoutpubsub.navbarWidth = layoutpubsub.setNavbarWidth;
        layoutpubsub.asideWidth = layoutpubsub.setAsideWidth;
        $('.nav-container').css('width', layoutpubsub.navbarWidth);
        $('.aside-container').css('width', layoutpubsub.asideWidth);
        layoutpubsub.navArrowAnimationSequence = layoutpubsub.getRightWobbleSequence($('#nav-arrow'));
        layoutpubsub.asideArrowAnimationSequence = layoutpubsub.getRightWobbleSequence($('#aside-arrow'));
    },

    applyBindings: function () {
        $(document).on('click', '#toggleAside', layoutpubsub.toggleAside);
        $(document).on('click', '#toggleNavbar', layoutpubsub.toggleNavbar);

        $(document).on('navToggled', layoutpubsub.toggleNavArrow);
        $(document).on('asideToggled', layoutpubsub.toggleAsideArrow);

        $(document).on('mouseenter', '.header-section-left', layoutpubsub.navArrowWobbleStart);
        $(document).on('mouseleave', '.header-section-left', layoutpubsub.navArrowWobbleEnd);
        $(document).on('click', '.header-section-left', layoutpubsub.toggleNavbar);

        $(document).on('mouseenter', '.header-section-right', layoutpubsub.asideArrowMouseEnter);
        $(document).on('mouseleave', '.header-section-right', layoutpubsub.asideArrowMouseLeave);
        $(document).on('click', '.header-section-right', layoutpubsub.toggleAside);

        $(document).on('mouseenter', 'nav > .nav-container > ul > li:not(.active)', layoutpubsub.navLinkHoverStart);
        $(document).on('mouseleave', 'nav > .nav-container > ul > li:not(.active)', layoutpubsub.navLinkHoverEnd);

        $(document).on('click', 'aside > .aside-container > .aside-tabs > ul > li', layoutpubsub.tabClick);
    },

    // ===== Animation ===== //     

    getLeftWobbleSequence: function ($element) {
        var animationSequence =
            [
                { e: $element, p: { rotateZ: 195 }, o: { duration: 100 } },
                { e: $element, p: { rotateZ: 170 }, o: { duration: 100 } },
                { e: $element, p: { rotateZ: 185 }, o: { duration: 100 } },
                { e: $element, p: { rotateZ: 175 }, o: { duration: 100 } },
                { e: $element, p: { rotateZ: 180 }, o: { duration: 100 } }
            ];
        return animationSequence;
    },

    getRightWobbleSequence: function ($element) {
        var animationSequence =
            [
                { e: $element, p: { rotateZ: 15 }, o: { duration: 100 } },
                { e: $element, p: { rotateZ: -10 }, o: { duration: 100 } },
                { e: $element, p: { rotateZ: 5 }, o: { duration: 100 } },
                { e: $element, p: { rotateZ: -5 }, o: { duration: 100 } },
                { e: $element, p: { rotateZ: 0 }, o: { duration: 100 } }
            ];
        return animationSequence;
    },

    getPulseSequence: function ($element) {
        var animationSequence =
            [
                { e: $element, p: { scaleX: 1.2, scaleY: 1.2 }, o: { duration: 50, easing: 'easeInExpo' } },
                { e: $element, p: { scaleX: 1, scaleY: 1 }, o: { duration: 200 } }
            ];
        return animationSequence;
    },


    // ===== Navbar ===== //

    toggleNavbar: function () {

        var easing = '';

        if (layoutpubsub.navbarIsVisible) {
            layoutpubsub.navbarIsVisible = false;
            layoutpubsub.navbarWidth = '0px';
            easing = 'ease-out';
        } else {
            layoutpubsub.navbarIsVisible = true;
            layoutpubsub.navbarWidth = layoutpubsub.setNavbarWidth;
            easing = 'ease-in';
            $('nav > .nav-container').show();
        }
        $('nav > .nav-container')
            .velocity('stop', true)
            .css({ 'overflow': 'hidden' })
            .velocity(
            { width: layoutpubsub.navbarWidth },
            {
                duration: layoutpubsub.setTransitionTime,
                easing: easing,
                complete: function () {
                    if (!layoutpubsub.navbarIsVisible) { $('nav > .nav-container').hide(); }
                    $('#body-grid').css('grid-template-columns', layoutpubsub.navbarWidth + ' auto ' + layoutpubsub.asideWidth);
                    $('nav > .nav-container').css({ 'overflow': 'visible' });
                }
            });
        $(document).trigger('navToggled');
    },


    // ===== Navbar Arrow ===== //

    toggleNavArrow: function () {
        var $arrow = $('#nav-arrow');
        if (!layoutpubsub.navbarIsVisible) {
            //Set the wobble animation for the hover event
            layoutpubsub.navArrowAnimationSequence = layoutpubsub.getLeftWobbleSequence($arrow);
            $arrow
                .removeClass('wobble')
                .velocity('stop', true)
                .velocity({ rotateZ: '180deg' }, { duration: 200 });
        } else {
            //Set the wobble animation for the hover event
            layoutpubsub.navArrowAnimationSequence = layoutpubsub.getRightWobbleSequence($arrow);
            $arrow
                .removeClass('wobble')
                .velocity('stop', true)
                .velocity({ rotateZ: '0deg' }, 200);
        }
    },
    navArrowWobbleStart: function () {
        $('#nav-arrow').addClass('wobble');
        $('#nav-arrow').velocity('stop', true);
        $.Velocity.RunSequence(layoutpubsub.navArrowAnimationSequence);
    },
    navArrowWobbleEnd: function () {
        $('#nav-arrow').removeClass('wobble');
        $('#nav-arrow.wobble')
            .velocity('stop');
    },


    // ===== Navbar Links ===== //

    navLinkHoverStart: function (el) {
        var $link = $(this);
        $link
            .velocity('stop', true)
            .velocity({ scaleX: 1.2, scaleY: 1.2 }, { duration: 50, easing: 'easeInExpo' });
    },

    navLinkHoverEnd: function (el) {
        var $link = $(this);
        $link.velocity({ scaleX: 1, scaleY: 1 }, { duration: 150 });
    },


    // ===== Aside ===== //

    toggleAside: function () {

        var easing = '';

        if (layoutpubsub.asideIsVisible) {
            layoutpubsub.asideIsVisible = false;
            layoutpubsub.asideWidth = '0px';
            easing = 'ease-out';
        } else {
            layoutpubsub.asideIsVisible = true;
            layoutpubsub.asideWidth = layoutpubsub.setAsideWidth;
            easing = 'ease-in';
            $('aside > .aside-container').show();
        }
        $('aside > .aside-container')
            .velocity('stop', true)
            .css({ 'overflow': 'hidden' })
            .velocity(
            { width: layoutpubsub.asideWidth },
            {
                duration: layoutpubsub.setTransitionTime,
                easing: easing,
                complete: function () {
                    if (!layoutpubsub.asideIsVisible) { $('aside > .aside-container').hide(); }
                    $('#body-grid').css('grid-template-columns', layoutpubsub.navbarWidth + ' auto ' + layoutpubsub.asideWidth);
                    $('aside > .aside-container').css({ 'overflow': 'visible' });
                }
            });
        $(document).trigger('asideToggled');
    },


    // ===== Aside Arrow ===== //

    toggleAsideArrow: function () {
        var $arrow = $('#aside-arrow');
        if (!layoutpubsub.asideIsVisible) {
            //Set the wobble animation for the hover event
            layoutpubsub.asideArrowAnimationSequence = layoutpubsub.getLeftWobbleSequence($arrow);
            $arrow
                .removeClass('wobble')
                .velocity('stop', true)
                .velocity({ rotateZ: '180deg' }, { duration: 200 });
        } else {
            //Set the wobble animation for the hover event
            layoutpubsub.asideArrowAnimationSequence = layoutpubsub.getRightWobbleSequence($arrow);
            $arrow
                .removeClass('wobble')
                .velocity('stop', true)
                .velocity({ rotateZ: '0deg' }, 200);
        }
    },
    asideArrowMouseEnter: function () {
        $('#aside-arrow').addClass('wobble');
        $('#aside-arrow').velocity('stop', true);
        $.Velocity.RunSequence(layoutpubsub.asideArrowAnimationSequence);
    },
    asideArrowMouseLeave: function () {
        $('#aside-arrow').removeClass('wobble');
        $('#aside-arrow.wobble')
            .velocity('stop');
    },


    // ===== Aside Tabs ===== //

    tabClick: function (el) {
        /*Could be done via css but the transition looks smoother when the active class is added AFTER the animation has ended*/
        var $deselectedTab = $('aside > .aside-container > .aside-tabs > ul > li.active');
        var $selectedTab = $(this);
        if (!$selectedTab.hasClass('active')) {

            $deselectedTab
                .velocity(
                { backgroundColor: '#7fffc1' },
                {
                    easing: 'easeOutQuad',
                    duration: 100,
                    complete: function () {
                        $deselectedTab
                            .removeClass('active')
                            .css({ 'background': '' });
                    }
                });

            $selectedTab
                .velocity(
                { backgroundColor: '#f7f7f7' },
                {
                    duration: 100,
                    easing: 'easeOutQuad',
                    complete: function () {
                        $selectedTab
                            .addClass('active')
                            .css({ 'background': '' });
                    }
                });
        }
    }

};
