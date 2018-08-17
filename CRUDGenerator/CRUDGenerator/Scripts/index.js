$(document).ready(function () {
    $("[role='collapse'], [role='editTable'], [role='editColumn'], [role='removeColumn'], [role='removeTable']").css("cursor", "pointer");
    $("[role='collapse']").click(function () {
        var target = $(this).attr("target");
        $(target).collapse("toggle");
    });

    $("[role='removeColumn']").click(function () {
        var database = $(this).attr("database");
        var table = $(this).attr("table");
        var column = $(this).attr("column");

        swal({
            title: 'Êtes-vous sûr?',
            text: "Vous allez effacer la colone " + column + " de la table " + table + " de la BDD " + database + " !",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: "Oui, l'effacer",
            cancelButtonText: "Non, la garder",
            closeOnConfirm: false
        }).then(function (isConfirm) {
            if (isConfirm) {
                swal(
                  'Effacée!',
                  'La colone a été effacée.',
                  'success'
                );
            }
        })
    });
});