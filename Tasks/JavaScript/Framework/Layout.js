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
    setNavbarWidth: 150,
    setNavbarMinifiedWidth: 60,
    setAsideWidth: 350,
    setAsideTabWidth: 39,
    setTransitionTime: 300,
    // =========================== //    

    navbarIsExpanded: true,
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
        $(document).on('contentResized', layoutpubsub.resizeContent);
        $(document).on('asideToggled', layoutpubsub.toggleAsideArrow);

        $(document).on('mouseenter', '.header-section-left', layoutpubsub.navArrowWobbleStart);
        $(document).on('mouseleave', '.header-section-left', layoutpubsub.navArrowWobbleEnd);
        $(document).on('click', '.header-section-left', layoutpubsub.toggleNavbar);

        $(document).on('mouseenter', '.header-section-right', layoutpubsub.asideArrowMouseEnter);
        $(document).on('mouseleave', '.header-section-right', layoutpubsub.asideArrowMouseLeave);
        $(document).on('click', '.header-section-right', layoutpubsub.toggleAside);

        $(document).on('mouseenter', 'nav > .nav-container > ul > li:not(.active)', layoutpubsub.navLinkHoverStart);
        $(document).on('mouseleave', 'nav > .nav-container > ul > li:not(.active)', layoutpubsub.navLinkHoverEnd);

        $(document).on('click', 'aside > .aside-container > .aside-tabs > ul > li:not(.disabled)', layoutpubsub.tabClick);
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

    // ===== Content ===== //

    resizeContent: function () {

        var $body = $('#body-grid');
        var $content = $('#content');
        var contentWidth = $content.width();
        var navWidth = $('nav > .nav-container').width();
        var newSize = $(window).width() - layoutpubsub.navbarWidth - layoutpubsub.asideWidth;

        $content.velocity('stop', true);
        $body.velocity('stop', true);

        if (navWidth !== layoutpubsub.navbarWidth) {
            $content.addClass('float-right');
        }

        if (newSize > contentWidth) {
            $body.css('grid-template-columns',
                layoutpubsub.navbarWidth + 'px auto ' + layoutpubsub.asideWidth + 'px');
        }
        
        $content
            .width(contentWidth)
            .velocity(
                { width: newSize + 'px' },
                {
                    easing: 'easeInSine',
                    complete: function() {
                        $(this)
                            .removeClass('float-right')
                            .css('width', '');
                        if (newSize <= contentWidth) {
                            $body.css('grid-template-columns',
                                layoutpubsub.navbarWidth + 'px auto ' + layoutpubsub.asideWidth + 'px');
                        }
                    }
                });
    },

    // ===== Navbar ===== //

    toggleNavbar: function () {
        var $text = $('nav > .nav-container ul > li a span'); //Text shown on icons
        var $nav = $('nav > .nav-container'); //Navbar pane

        $nav.velocity('stop', true);
        $text.velocity('stop', true);

        //Minify navbar
        if (layoutpubsub.navbarIsExpanded) {
            layoutpubsub.navbarIsExpanded = false;
            layoutpubsub.navbarWidth = layoutpubsub.setNavbarMinifiedWidth;
            //Hide text then squash pane
            $text.velocity('fadeOut',
                {
                    duration: 100,
                    complete: function () {
                        $nav.velocity(
                            { width: layoutpubsub.navbarWidth + 'px'},
                            {
                                duration: 400,
                                easing: 'spring'
                            });
                        $(document).trigger('contentResized');
                        $(document).trigger('navToggled');
                    }
                });
        } else {

        //Expand Navbar
            layoutpubsub.navbarIsExpanded = true;
            layoutpubsub.navbarWidth = layoutpubsub.setNavbarWidth;
            //Extend pane then show text
            $nav.velocity( { width: layoutpubsub.navbarWidth + 'px' },
                {
                    duration: 400,
                    easing: 'spring',
                    complete: function () {
                        $text.velocity('fadeIn', { duration: 100 });
                    }
                });
            $(document).trigger('contentResized');
            $(document).trigger('navToggled');
        }
    },


    // ===== Navbar Arrow ===== //

    toggleNavArrow: function () {
        var $arrow = $('#nav-arrow');
        if (!layoutpubsub.navbarIsExpanded) {
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
    defaultTab: function() {

        
    },

    toggleAside: function () {

        var easing = '';
        var $pane = $('aside > .aside-container');
        var $content = $('#aside-content').find('*');

        $pane.velocity('stop', true);
        $content.velocity('stop', true);

        //Minify aside

        if (layoutpubsub.asideIsVisible) {
            layoutpubsub.asideIsVisible = false;
            layoutpubsub.asideWidth = layoutpubsub.setAsideTabWidth;


            $content.velocity('fadeOut',
                {
                    duration: 100,
                    complete: function() {
                        $pane.velocity(
                            { width: layoutpubsub.asideWidth + 'px'} ,
                            {
                                duration: 400,
                                easing: 'spring'
                            });
                        $(document).trigger('contentResized');
                        $(document).trigger('asideToggled');
                    }
                });

        } else {

            //Expand aside
            layoutpubsub.asideIsVisible = true;
            layoutpubsub.asideWidth = layoutpubsub.setAsideWidth;

            $pane.velocity(
                {
                    width: layoutpubsub.asideWidth
                },
                {
                    duration: 450,
                    easing: 'spring',
                    complete: function() {
                        $content.velocity('fadeIn', 100);
                    }
                });
            $(document).trigger('contentResized');
            $(document).trigger('asideToggled');
        }
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
