

$(function() {
    planWeekPubsub.init();
});

var planWeekPubsub = {

    init: function () {
        $('#open-container').niceScroll({
            cursorcolor: "aquamarine",
            cursorwidth: "16px",
            bouncescroll: true,
            smoothscroll: true,
            railalign: 'left'
        });

        $('#week-container').niceScroll({
            cursorcolor: "aquamarine",
            cursorwidth: "16px",
            bouncescroll: true,
            smoothscroll: true,
            railalign: 'left'
        });
        /*
            Implement when divs are finished
            Split(['#weekPlan', '#draggables'], {
                direction:'vertical',
                sizes: [29, 71],
                minSize: 75
            });
        */
    }
}
