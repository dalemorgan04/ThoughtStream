$(function () {
    menupubsub.init();
    menupubsub.applyBindings();
});

var menupubsub = {
    init: function () {
        $('#sidebar').niceScroll({
            cursorcolor: '#53619d', // Changing the scrollbar color
            cursorwidth: 4, // Changing the scrollbar width
            cursorborder: 'none' // Rempving the scrollbar border
        });
    },

    applyBindings: function () {
        $(document).on('click', '#sidebarCollapse', this.toggleSidebar);
    },

    toggleSidebar: function() {
        $('#sidebar').toggleClass('active');
        // close dropdowns
        $('.collapse.in').toggleClass('in');
        // and also adjust aria-expanded attributes we use for the open/closed arrows
        // in our CSS
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    }
}

