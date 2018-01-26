/// <reference path="../../framework/jquery.nicescroll.js" />
/// <reference path="../../framework/jquery-ui-1.12.1.js" />


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
            cursorborder: '0px transparent',
            cursorwidth: '10px',
            bouncescroll: true,
            smoothscroll: true,
            railalign: 'left'
        });

        $('.connectedSortable').sortable({
            connectWith: '.connectedSortable',
            scroll: true,
            cursor: 'move',
            cursorAt: { top: 40, left: 60 },
            helper: 'original',
            zIndex: 9999,
            containment: 'window'
        }).disableSelection();

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
