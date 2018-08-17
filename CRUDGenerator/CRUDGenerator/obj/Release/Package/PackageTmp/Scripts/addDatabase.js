$(function () {
    $("#addDatabase-form").submit(function (event) {
        var databaseName = $("#addDatabase-databaseName").val();
        $.ajax({
            url: 'ajax/UserDatabases/addDatabase', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: { Name: databaseName },
            dataType: 'json',
            success: function (returnDat) {
                location.reload();
            }
        });
    });
});