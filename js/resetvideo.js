$('.modal').on('hidden.bs.modal', function () {
    $('#footage').trigger('pause');
});
