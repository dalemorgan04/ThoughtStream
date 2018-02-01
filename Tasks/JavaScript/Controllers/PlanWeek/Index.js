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
            items: '>*:not(.emptyTablePlaceholder)',
            connectWith: '.connectedSortable',
            scroll: true,
            cursor: 'move',
            cursorAt: { top: 40, left: 60 },
            helper: 'clone',
            appendTo: document.body,
            zIndex: 10000,
            placeholder: 'placeholder',
            dropOnEmpty: true,
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
        //TODO When the left pane is minimised you need to reset the position of the 
        //scrollbars or they can be left floating in the middle of the page
    },


    }

}
