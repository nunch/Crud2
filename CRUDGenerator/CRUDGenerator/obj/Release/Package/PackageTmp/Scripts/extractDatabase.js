function open(path, params, method) {
    method = method || "post"; // Set method to post by default if not specified.

    // The rest of this code assumes you are not using a library.
    // It can be made less wordy if you use one.
    var form = document.createElement("form");
    form.setAttribute("method", method);
    form.setAttribute("action", path);

    for (var key in params) {
        if (params.hasOwnProperty(key)) {
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("type", "hidden");
            hiddenField.setAttribute("name", key);
            hiddenField.setAttribute("value", params[key]);

            form.appendChild(hiddenField);
        }
    }

    document.body.appendChild(form);
    form.submit();
}
function post(path, params) {
    open(path, params, "post");
}
function get(path, params) {
    open(path, params, "get");
}
$(function () {
    $(".chosen-select").chosen({ no_results_text: "Aucune bdd de ce nom", width: "95%" });
    $("#extractDatabase-code").click(function (event) {
        event.preventDefault();
        var databaseName = $("#extractDatabase-databaseName").val();
        var codeType = $("#extractDatabase-codeType").val();
        post("/ajax/database/extractCode", { databaseName: databaseName, codeType: codeType });
        /*$.ajax({
            url: 'ajax/database/extractCode', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: { databaseName: databaseName, codeType: codeType },
            success: function (url) {
                window.open(url);
            }
        });*/
    });

    $("#extractDatabase-script").click(function (event) {
        event.preventDefault();
        var databaseName = $("#extractDatabase-databaseName").val();
        var scriptType = $("#extractDatabase-scriptType").val();

        post("/ajax/database/extractScript", { databaseName: databaseName, scriptType: scriptType });
        /*$.ajax({
            url: 'ajax/database/extractScript', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: { databaseName: databaseName, scriptType: scriptType },
            success: function (url) {
                window.open(url);
            }
        });*/
    });

    $("#extractDatabase-all").click(function (event) {
        event.preventDefault();
        var databaseName = $("#extractDatabase-databaseName").val();
        var codeType = $("#extractDatabase-codeType").val();
        var scriptType = $("#extractDatabase-scriptType").val();

        post("/ajax/database/extractAll", { databaseName: databaseName, codeType: codeType, scriptType: scriptType });
        /*$.ajax({
            url: 'ajax/database/extractAll', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: { databaseName: databaseName, codeType: codeType, scriptType: scriptType },
            success: function (url) {
                window.open(url);
            }
        });*/
    });
});