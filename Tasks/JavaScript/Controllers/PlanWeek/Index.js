/// <reference path="../../framework/jquery.nicescroll.js" />


$(function() {
    planWeekPubsub.init();
});

var planWeekPubsub = {

    init: function () {
        $('#open-container .plan .card').niceScroll('.list',{
            cursorcolor: 'aquamarine',
            cursorborder: '0px transparent',
            cursorwidth: '10px',
            bouncescroll: true,
            smoothscroll: true,
            railalign: 'left'
        });
        /*$('#week-container .plan').niceScroll(".card");*/

        
        $('#week-container .plan').niceScroll({
            cursorcolor: 'aquamarine',
            cursorwidth: '1px',
            bouncescroll: true,
            smoothscroll: true,
            railalign: 'left',
            railpadding: {left:10}

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
