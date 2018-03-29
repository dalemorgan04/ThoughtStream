$(function () {
    thoughtsPubsub.init();
    thoughtsPubsub.applyBindings();
});

var thoughtsPubsub = {

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
            axis:'y',
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
                $.post(thoughtsPubsub.urls.MoveThought,
                    { thoughtId: thoughtId , moveToSortId: moveToSortId},
                    function(result) {
                        if (result == "True" || result === true) {
                            thoughtsPubsub.getThoughtsTable();
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
        $(document).on('mouseenter', '#thoughtResultsTableBody > tr', this.showOptions);
        $(document).on('mouseleave', '#thoughtResultsTableBody > tr', this.hideOptions);
        $(document).on('click', '#thoughtResultsTableBody > tr', this.select);
    },

    deleteThought: function(e) {
        $.post(
            thoughtsPubsub.urls.DeleteThought,
            { thoughtId: thoughtsPubsub.getThougtId(e) },
            function(result) {
                if (result == "True" || result === true) {
                    thoughtsPubsub.getThoughtsTable();
                }
            });
    },

    getThoughtsTable: function() {
        $.post(thoughtsPubsub.urls.GetThoughtsTable,
            function(resultHtml) {
                $('#thoughtResultsTable').html(resultHtml);
                thoughtsPubsub.init();
            });
    },

    hideOptions: function(el) {
        $(el.target)
            .closest('tr')
            .find('.options')
            .removeClass('visible');
    },

    showOptions: function(el) {
        $(el.target)
            .closest('tr')
            .find('.options')
            .addClass('visible');
    },

    showAsideOptions: function() {
        var $selection = $('#thoughtResultsTableBody .selected');
        if ($selection.length()) {
            //Something selected
            $('#aside-tabs-edit').removeClass('disabled');
        } else {
            //Nothing selected
            $('#aside-tabs-edit').addClass('disabled');
        }    
    },

    select: function (el) {
        $('#thoughtResultsTableBody .selected').removeClass('selected');
        $(el.target)
            .closest('tr')
            .addClass('selected');
        $(document).trigger('selected');
    },
      
    saveThought: function (e) {
        e.preventDefault();
        var viewModel = $('#addThoughtForm').serialize();
        $.post(thoughtsPubsub.urls.SaveThought, viewModel,
            function(result) {
                if (result == "True" || result === true) {
                    thoughtsPubsub.getThoughtsTable();
                }
            });
    },

    movedThought: function() {
        
    },

    validateForm: function(e) {
        return true;
    }
};