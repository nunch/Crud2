function contains(a, obj) {
    for (var i = 0; i < a.length; i++) {
        if (a[i] == obj) {
            return true;
        }
    }
    return false;
}
function createChildRow(data, id) {
}
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
                $.ajax({
                    url: 'ajax/table/removeColumn', // La ressource ciblée
                    type: 'POST', // Le type de la requête HTTP.
                    data: { databaseName: database, tableName: table, columnName: column },
                    dataType: 'json',
                    success: function (returnDat) {
                        if (returnDat != "") {
                            swal('Problème', returnDat, 'error');
                        } else {
                            $("#columnsTabTable").DataTable().clear();
                            $.ajax({
                                url: 'ajax/table/getTableColumns', // La ressource ciblée
                                type: 'POST', // Le type de la requête HTTP.
                                data: { databaseName: databaseName, tableName: tableName },
                                dataType: 'json',
                                success: function (returnDat) {
                                    $("#columnsTabTable").DataTable().rows.add(returnDat).draw();
                                }
                            });
                        }
                    }
                });
            }
        })
    });

    $("#table-type").change(function () {

        var database = $("#databaseName").val();
        var table = $("#tableName").val();
        $.ajax({
            url: 'ajax/table/changeType', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: { databaseName: database, tableName: table, type: $("#table-type").val() },
            dataType: 'json',
            success: function (returnDat) {
            }
        });
    })

    $("[role='removeForeignKey']").click(function () {
        var database = $(this).attr("database");
        var table = $(this).attr("table");
        var foreignKey = $(this).attr("foreignKey");

        swal({
            title: 'Êtes-vous sûr?',
            text: "Vous allez effacer la clé étrangère " + foreignKey + " de la table " + table + " de la BDD " + database + " !",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: "Oui, l'effacer",
            cancelButtonText: "Non, la garder",
            closeOnConfirm: false
        }).then(function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    url: 'ajax/table/removeForeignKey', // La ressource ciblée
                    type: 'POST', // Le type de la requête HTTP.
                    data: { databaseName: database, tableName: table, fkName: foreignKey },
                    dataType: 'json',
                    success: function (returnDat) {
                        location.reload();
                    }
                });
            }
        })
    });


    $("[role='removeUniqueKey']").click(function () {
        var database = $(this).attr("database");
        var table = $(this).attr("table");
        var ukName = $(this).attr("uniqueKey");

        swal({
            title: 'Êtes-vous sûr?',
            text: "Vous allez effacer la clé unique " + ukName + " de la table " + table + " de la BDD " + database + " !",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: "Oui, l'effacer",
            cancelButtonText: "Non, la garder",
            closeOnConfirm: false
        }).then(function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    url: 'ajax/table/removeUniqueKey', // La ressource ciblée
                    type: 'POST', // Le type de la requête HTTP.
                    data: { databaseName: database, tableName: table, ukName: ukName },
                    dataType: 'json',
                    success: function (returnDat) {
                        location.reload();
                    }
                });
            }
        })
    });


    $("[role='removeIndex']").click(function () {
        var database = $(this).attr("database");
        var table = $(this).attr("table");
        var index = $(this).attr("index");

        swal({
            title: 'Êtes-vous sûr?',
            text: "Vous allez effacer l'index " + index + " de la table " + table + " de la BDD " + database + " !",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: "Oui, l'effacer",
            cancelButtonText: "Non, la garder",
            closeOnConfirm: false
        }).then(function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    url: 'ajax/table/removeIndex', // La ressource ciblée
                    type: 'POST', // Le type de la requête HTTP.
                    data: { databaseName: database, tableName: table, indexName: index },
                    dataType: 'json',
                    success: function (returnDat) {
                        location.reload();
                    }
                });
            }
        })
    });

    $("#modal-addForeignKey-externTableName").change(function () {
        var databaseName = $("#databaseName").val();
        var tableName = $("#modal-addForeignKey-externTableName").val();
        console.log(databaseName);
        console.log(tableName);
        $.ajax({
            url: 'ajax/database/getTableByName', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: { databaseName: databaseName, tableName: tableName },
            dataType: 'json',
            success: function (returnDat) {
                returnDat = JSON.parse(returnDat);
                console.log(returnDat)
                var columns = returnDat["Columns"];
                $("#modal-addForeignKey-externTable [name='ExternColumnName']").html("");
                $("#modal-addForeignKey-externTable [name='ExternColumnName']").append("<option disabled selected>Colone...</option>");
                for (var i = 0; i < columns.length; i++) {
                    var columnName = columns[i]["Name"];
                    if (contains(returnDat["PrimaryKey"], columnName)) {
                        var html = "<option value='" + columnName + "'>" + columnName + "</option>";
                        $("#modal-addForeignKey-externTable [name='ExternColumnName']").append(html);
                    }
                }

            }
        });
    });
    $("#modal-addForeignKey-submit").click(function (event) {
        event.preventDefault();
        var databaseName = $("#databaseName").val();
        var tableName = $("#tableName").val();
        var form = document.getElementById('modal-addForeignKey-form');
        var formData = new FormData(form);
        formData.append("databaseName", databaseName);
        formData.append("tableName", tableName);
        $.ajax({
            url: 'ajax/table/addForeignKey', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (returnDat) {
                location.reload();
            }
        });
    });

    $("#modal-addIndex-submit").click(function (event) {
        event.preventDefault();
        var databaseName = $("#databaseName").val();
        var tableName = $("#tableName").val();
        var form = document.getElementById('modal-addIndex-form');
        var formData = new FormData(form);
        formData.append("databaseName", databaseName);
        formData.append("tableName", tableName);
        $.ajax({
            url: 'ajax/table/addIndex', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (returnDat) {
                location.reload();
            }
        });
    });

    $("#modal-addUniqueKey-submit").click(function (event) {
        event.preventDefault();
        var databaseName = $("#databaseName").val();
        var tableName = $("#tableName").val();
        var form = document.getElementById('modal-addUniqueKey-form');
        var formData = new FormData(form);
        formData.append("databaseName", databaseName);
        formData.append("tableName", tableName);
        $.ajax({
            url: 'ajax/table/addUniqueKey', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (returnDat) {
                location.reload();
            }
        });
    });



    $("#modal-addPrimaryKey-submit").click(function (event) {
        event.preventDefault();
        var databaseName = $("#databaseName").val();
        var tableName = $("#tableName").val();
        var form = document.getElementById('modal-addPrimaryKey-form');
        var formData = new FormData(form);
        formData.append("databaseName", databaseName);
        formData.append("tableName", tableName);
        $.ajax({
            url: 'ajax/table/addPrimaryKey', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (returnDat) {
                location.reload();
            }
        });
    });


    $("#removePrimaryKey").click(function (event) {
        event.preventDefault();
        var databaseName = $("#databaseName").val();
        var tableName = $("#tableName").val();
        $.ajax({
            url: 'ajax/table/removePrimaryKey', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: { databaseName: databaseName, tableName: tableName },
            dataType: 'json',
            success: function (returnDat) {
                location.reload();
            }
        });
    });

    $("#modal-addColumn-submit").click(function (event) {
        event.preventDefault();
        var databaseName = $("#databaseName").val();
        var tableName = $("#tableName").val();
        var form = document.getElementById('modal-addColumn-form');
        var formData = new FormData(form);
        formData.append("databaseName", databaseName);
        formData.append("tableName", tableName);
        var isNull = $("#modal-addColumn-columnIsNull")[0].checked
        if (isNull) {
            isNull = "on"
        } else {
            isNull = "off"
        }
        var dataToSend = {
            "databaseName": databaseName,
            "tableName": tableName,
            "Name": $("#modal-addColumn-columnName").val(),
            "columnType": $('[name="columnType"]').val(),
            "Length": $("#modal-addColumn-columnLength").val(),
            "IsNull": isNull
        };
        $.ajax({
            url: 'ajax/table/addColumn', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: dataToSend,
            dataType: 'json',
            success: function (returnDat) {
                $("#columnsTabTable").DataTable().clear();
                $.ajax({
                    url: 'ajax/table/getTableColumns', // La ressource ciblée
                    type: 'POST', // Le type de la requête HTTP.
                    data: { databaseName: databaseName, tableName: tableName },
                    dataType: 'json',
                    success: function (returnDat) {
                        $("#columnsTabTable").DataTable().rows.add(returnDat).draw();
                    }
                });
            }
        });
    })
    var databaseName = $("#databaseName").val();
    var tableName = $("#tableName").val();
    var columns = [
        { "sClass": "", input: "select", values:"default", "sTitle": "Nom", "mData": "Name", "sDefaultContent": "-", "bVisible": true },
        { "sClass": "", input: "text", "sTitle": "Type", "mData": "Type", "sDefaultContent": "-", "bVisible": true },
        {
            "sClass": "", input: "text", "sTitle": "Primary Key", "mData": function (source, type, val) {
                if (source.PK != -1) {
                    return '<i class="fa fa-key fa-2"></i>';
                }
                return '/';
            }, "sDefaultContent": "-", "bVisible": true
        },
        {
            "sClass": "", input: "text", "sTitle": "Longueur", "mData": function (source, type, val) {
                return '/';
            }, "sDefaultContent": "-", "bVisible": true
        },
        {
            "sClass": "jumbotron-icon", "sTitle": "Action", "mData": function (source, type, val) {
                return '<i class="fa fa-times fa-2" role="removeColumn" database="' + databaseName + '" table="' + tableName + '" column="' + source.Name + '"></i>';
            }, "sDefaultContent": "-", "bVisible": true
        },
    ]
    initDataTable($("#columnsTabTable"), columns, [], {
        "drawCallback": function (settings) {
            $('#columnsTabTable openChild').click(function(){
                var tr = $(this).closest("tr");
                var data = $('#columnsTabTable').DataTable().row(tr).data();
                var id = $('#columnsTabTable').DataTable().row(tr).id();

            })
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
                        $.ajax({
                            url: 'ajax/table/removeColumn', // La ressource ciblée
                            type: 'POST', // Le type de la requête HTTP.
                            data: { databaseName: database, tableName: table, columnName: column },
                            dataType: 'json',
                            success: function (returnDat) {
                                if (returnDat != "") {
                                    swal('Problème', returnDat, 'error');
                                } else {
                                    location.reload();
                                }
                            }
                        });
                    }
                })
            });
        }
    });
    $.ajax({
        url: 'ajax/table/getTableColumns', // La ressource ciblée
        type: 'POST', // Le type de la requête HTTP.
        data: { databaseName: databaseName, tableName: tableName },
        dataType: 'json',
        success: function (returnDat) {
            $("#columnsTabTable").DataTable().rows.add(returnDat).draw();
        }
    });
});
var index = {
    'addColumn': function () {
        var html = $("#modal-addIndex-copy").html();
        $("#modal-addIndex-row").append(html);
        $("#modal-addIndex-row select").removeAttr("disabled");
    },
    'removeColumn': function (selector) {
        $(selector).parent().remove();
    }
}
var uniqueKey = {
    'addColumn': function () {
        var html = $("#modal-addUniqueKey-copy").html();
        $("#modal-addUniqueKey-row").append(html);
        $("#modal-addUniqueKey-row select").removeAttr("disabled");
    },
    'removeColumn': function (selector) {
        $(selector).parent().remove();
    }
}
var foreignKey = {
    'addColumn': function () {
        var html = $("#modal-addForeignKey-copy").html();
        $("#modal-addForeignKey-tbody").append(html);
        $("#modal-addForeignKey-tbody select").removeAttr("disabled");
    },
    'removeColumn': function (selector) {
        $(selector).parent().parent().remove();
    }
}
var primaryKey = {
    'addColumn': function () {
        var html = $("#modal-addPrimaryKey-copy").html();
        $("#modal-addPrimaryKey-row").append(html);
        $("#modal-addPrimaryKey-row select").removeAttr("disabled");
    },
    'removeColumn': function (selector) {
        $(selector).parent().remove();
    }
}