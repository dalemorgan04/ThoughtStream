$(function () {
    pubsub.init();
    pubsub.applyBindings();
});

var pubsub = {

    getThougtId: function (e) { return $(e.target).closest('tr')[0].dataset.thoughtid; },

    urls: {
        DeleteThought: Core.ResolveUrl('DeleteThought', 'Thoughts'),
        GetThoughtsTable: Core.ResolveUrl('GetThoughtsTable', 'Thoughts'),
        SaveThought: Core.ResolveUrl('Create', 'Thoughts'),
        MoveThought: Core.ResolveUrl('MoveThought', 'Thoughts')
    },

    init: function() {
        $('#thoughtResultsTableBody').sortable({tolerance:'pointer'});
        $('#thoughtResultsTableBody').disableSelection();
        $('#thoughtResultsTableBody').sortable({
            update: function (event, ui) {
                var line = ui.item.context;
                var thoughtId = line.dataset.thoughtid;
                var prevRow = line.previousElementSibling;
                var moveToSortId = 0;
                /*
                    If the line is dragged upwards then the previous line method returns +1 more than you'd expect
                    For this reason we need to work out the sortid differently depending on if the line is moving
                    upwards and downwards
                */
                if (prevRow != null) {
                    if (line.startRow < line.rowIndex) {
                        moveToSortId = prevRow.dataset.sortid;    
                    } else {
                        moveToSortId = 1 + (+ prevRow.dataset.sortid);    
                    }
                };
                $.post(pubsub.urls.MoveThought,
                    { thoughtId: thoughtId , moveToSortId: moveToSortId},
                    function(result) {
                        if (result == "True" || result === true) {
                            pubsub.getThoughtsTable();
                        }
                    }
                );
            },
            start: function(e, ui) {
                ui.item.context.startRow = ui.item.context.rowIndex;
            }
        });
    },

    applyBindings: function() {
        $(document).on('click', '#btnAddThought', this.saveThought);
        $(document).on('submit', '#addThoughtForm', this.saveThought);
        $(document).on('click', '#btnClose', this.deleteThought);
    },

    deleteThought: function(e) {
        $.post(
            pubsub.urls.DeleteThought,
            { thoughtId: pubsub.getThougtId(e) },
            function(result) {
                if (result == "True" || result === true) {
                    pubsub.getThoughtsTable();
                }
            });
    },

    getThoughtsTable: function() {
        $.post(pubsub.urls.GetThoughtsTable,
            function(resultHtml) {
                $('#thoughtResultsTable').html(resultHtml);
                pubsub.init();
            });
    },
      
    saveThought: function (e) {
        e.preventDefault();
        var viewModel = $('#addThoughtForm').serialize();
        $.post(pubsub.urls.SaveThought, viewModel,
            function(result) {
                if (result == "True" || result === true) {
                    pubsub.getThoughtsTable();
                }
            });
    },

    movedThought: function() {
        
    },

    validateForm: function(e) {
        return true;
    }
};