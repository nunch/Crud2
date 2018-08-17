var dataTableInitSelectDataObjectsInitDataTable = {};
var dataTableInitSelectDataObjectsActiveSearch = {};
var paramLanguage = {
    "sProcessing": "Traitement en cours...",
    "sSearch": "Rechercher&nbsp;:",
    "sLengthMenu": "Afficher _MENU_ &eacute;l&eacute;ments",
    "sInfo": "<span class='Total'>Total </span><span class='TotalNumber'>_MAX_ &eacute;l&eacute;ment(s)</span>",
    "sInfoEmpty": "Affichage de l'&eacute;l&eacute;ment 0 &agrave; 0 sur 0 &eacute;l&eacute;ment",
    "sInfoFiltered": "_TOTAL_ trouv&eacute;(s)",
    "sInfoPostFix": "",
    "sLoadingRecords": "Chargement en cours...",
    "sZeroRecords": "Aucun &eacute;l&eacute;ment &agrave; afficher",
    "sEmptyTable": "Aucune donn&eacute;e disponible dans le tableau",
    "oPaginate": {
        "sFirst": "Premier",
        "sPrevious": "Pr&eacute;c&eacute;dent",
        "sNext": "Suivant",
        "sLast": "Dernier"
    },
    "oAria": {
        "sSortAscending": ": activer pour trier la colonne par ordre croissant",
        "sSortDescending": ": activer pour trier la colonne par ordre d&eacute;croissant"
    },
};
function contains(array, obj) {
    for (var i = 0; i < array.length; i++) {
        if (array[i] === obj) {
            return true;
        }
    }
    return false;
}
function replaceAll(str, find, replace) {
    try {
        return str.replace(new RegExp(find, 'g'), replace);
    } catch (e) {
        debugger
    }
}
function showHideDataTableColumns(table, dropdown) {
    var columns = $(table).DataTable().settings()[0].aoColumns
    var tmp = [];
    var columnToSearch = {};
    for (var i = 0; i < columns.length; i++) {
        if (columns[i].bVisible) {
            tmp.push(columns[i].idx);
            columnToSearch[columns[i].idx] = columns[i];
        }
    }
    tmp.sort(function (a, b) {
        return a - b;
    });

    for (var i = 0; i < tmp.length; i++) {
        $(dropdown).append('<li index="' + tmp[i] + '" class="checkedSquare"><label>' + columnToSearch[tmp[i]].sTitle + '</label></li>');
    }

    $(dropdown).find('li').click(function (event) {
        event.preventDefault();
        event.stopPropagation();
        var index = $(this).attr("index");
        var isVisible = $(this).hasClass("emptySquare");
        //var checkbox = $(this).find('[type="checkbox"]');
        //var isVisible = !$(checkbox).prop("checked");
        //$(checkbox).prop("checked", isVisible);
        if (isVisible) {
            $(this).removeClass("emptySquare");
            $(this).addClass("checkedSquare");
            $(table).find(".SearchInputs [index='" + index + "']").show();
        } else {
            $(this).addClass("emptySquare");
            $(this).removeClass("checkedSquare");
            $(table).find(".SearchInputs [index='" + index + "']").hide();
        }
        var column = $(table).DataTable().column(index);
        column.visible(!column.visible());
    })
}
function resetDataTableSearch(table) {
    $(table).find('thead .SearchInputs [inputSearch]').val("");
    $(table).DataTable().search('').columns().search('').draw();
}
function initDataTable(table, columns, order, additionnalData) {
    $(table).css("width", "100%")
    jQuery.fn.dataTableExt.oSort['date-fr-time-asc'] = function (x, y) {
        return (moment(x, "DD/MM/YYYY HH:mm").isAfter(moment(y, "DD/MM/YYYY HH:mm"))) ? 1 : 0;
    };

    jQuery.fn.dataTableExt.oSort['date-fr-time-desc'] = function (x, y) {
        return (moment(x, "DD/MM/YYYY HH:mm").isBefore(moment(y, "DD/MM/YYYY HH:mm"))) ? 1 : 0;
    };

    jQuery.fn.dataTableExt.oSort['date-fr-asc'] = function (x, y) {
        return (moment(x, "DD/MM/YYYY HH:mm").isAfter(moment(y, "DD/MM/YYYY"))) ? 1 : 0;
    };

    jQuery.fn.dataTableExt.oSort['date-fr-desc'] = function (x, y) {
        return (moment(x, "DD/MM/YYYY HH:mm").isBefore(moment(y, "DD/MM/YYYY"))) ? 1 : 0;
    };
    if (order == undefined)
        order = [];
    if (additionnalData == undefined)
        additionnalData = {};
    var dataTableOject = {
        data: [],
        dom: "rtp",
        "bDestroy": true,
        "language": paramLanguage,
        "pageLength": 10,
        order: order,
        "columns": columns,
        "fnCreatedRow": function (nRow, aData, iDataIndex) {
        },
        "headerCallbackTmp": function (nHead, aData, iStart, iEnd, aiDisplay) {
            $(table).DataTable().columns().iterator('column', function (settings, column) {
                if (settings.aoColumns[column].tooltip !== undefined) {
                    $($(table).DataTable().column(column).header()).attr('data-original-title', settings.aoColumns[column].tooltip);
                    $($(table).DataTable().column(column).header()).tooltip({ "container": "body" });
                }
            });
        },
        "drawCallbackTmp": function (settings) {
            var columns = $(table).DataTable().settings()[0].aoColumns
            var tmp = [];
            var columnToSearch = {};
            for (var i = 0; i < columns.length; i++) {
                if (columns[i].bVisible) {
                    tmp.push(columns[i].idx);
                    columnToSearch[columns[i].idx] = columns[i];
                }
            }
            for (var i = 0; i < tmp.length; i++) {
                if (columnToSearch[tmp[i]].input != undefined) {
                    if (columnToSearch[tmp[i]].input == "select") {
                        var values = columnToSearch[tmp[i]].values
                        if (values == "default") {
                            dataTableInitSelectDataObjectsInitDataTable[tmp[i]] = [""];
                            var rowData = $(table).DataTable().columns(tmp[i], { search: "applied" }).data()[0];
                            if ($(table).find(".SearchInputs [index=" + tmp[i] + "]") == undefined) {
                                dataTableInitSelectDataObjectsActiveSearch[tmp[i]] = "";
                                for (var j = 0; j < rowData.length; j++) {
                                    if (!contains(dataTableInitSelectDataObjectsInitDataTable[tmp[i]], rowData[j])) {
                                        dataTableInitSelectDataObjectsInitDataTable[tmp[i]].push(rowData[j]);
                                    }
                                }
                                dataTableInitSelectDataObjectsInitDataTable[tmp[i]].sort();
                            } else {
                                $(table).find(".SearchInputs [index=" + tmp[i] + "] select").html('<option value=""></option>');
                                var tmp2 = [];
                                for (var j = 0; j < rowData.length; j++) {
                                    if (rowData[j] != undefined && rowData[j] != "") {
                                        if (!contains(tmp2, rowData[j])) {
                                            tmp2.push(rowData[j]);
                                        }
                                    }
                                }
                                tmp2.sort();
                                for (var j = 0; j < tmp2.length; j++) {
                                    var value = replaceAll(tmp2[j], '"', '\\"');
                                    $(table).find(".SearchInputs [index=" + tmp[i] + "] select").append('<option value="' + value + '">' + tmp2[j] + '</option>');
                                }
                                $(table).find(".SearchInputs [index=" + tmp[i] + "] select").val(dataTableInitSelectDataObjectsActiveSearch[tmp[i]]);
                            }
                        }
                    }
                }
            }
        },
        "initCompleteTmp": function (settings) {
            //$(table).find('thead th[data-original-title]').tooltip({ "container": "body" });
            //$("[data-original-title]").tooltip();
            $(table).find('thead').append("<tr class='SearchInputs'></tr>");
            var columns = $(table).DataTable().settings()[0].aoColumns
            var tmp = [];
            var columnToSearch = {};
            for (var i = 0; i < columns.length; i++) {
                if (columns[i].bVisible) {
                    tmp.push(columns[i].idx);
                    columnToSearch[columns[i].idx] = columns[i];
                }
            }
            tmp.sort(function (a, b) {
                return a - b;
            });
            for (var i = 0; i < tmp.length; i++) {
                var html = "<th index='" + tmp[i] + "'></th>";
                if (columnToSearch[tmp[i]].input != undefined) {
                    if (columnToSearch[tmp[i]].input == "select") {
                        var options = "";
                        var values = columnToSearch[tmp[i]].values
                        if (values == "default") {
                            if (dataTableInitSelectDataObjectsInitDataTable[tmp[i]] != undefined) {
                                for (var j = 0; j < dataTableInitSelectDataObjectsInitDataTable[tmp[i]].length; j++) {
                                    var value = dataTableInitSelectDataObjectsInitDataTable[tmp[i]][j];
                                    var valueReplace = replaceAll(value, '"', '\\"');
                                    options += '<option value="' + valueReplace + '">' + value + '</option>';
                                }
                            }
                            html = '<th index="' + tmp[i] + '" style="padding: 2px;"><select inputSearch="select"><option value=""></option>' + options + '</select></th>';
                        } else {
                            for (var key in values) {
                                var keyReplace = replaceAll(key, '"', '\\"');
                                options += '<option value="' + keyReplace + '">' + values[key] + '</option>';
                            }
                            html = '<th index="' + tmp[i] + '" style="padding: 2px;"><select inputSearch="select"><option value=""></option>' + options + '</select></th>';
                        }
                    }
                    else if (columnToSearch[tmp[i]].input == "text") {
                        html = '<th index="' + tmp[i] + '" style="padding: 2px;"><input inputSearch="text" type="text" placeholder="Rechercher..."></th>';
                    }
                    else if (columnToSearch[tmp[i]].input == "checkbox") {
                        html = '<th index="' + tmp[i] + '" style="padding: 2px;"><select inputSearch="checkbox"><option value=""></option><option value="Oui">Oui</option><option value="Non">Non</option></select></th>';
                    }
                }
                $(table).find('thead .SearchInputs').append(html);

            }
            $(table).find('thead .SearchInputs [inputSearch=text]').on("keyup", function () {
                var index = $(this).parent().attr("index");
                var value = $(this).val();
                $(table).DataTable().column(index).search(value, true, false).draw()
            })
            $(table).find('thead .SearchInputs [inputSearch=select]').change(function () {
                var index = $(this).parent().attr("index");
                var value = $(this).val();
                dataTableInitSelectDataObjectsActiveSearch[index] = value;
                if (typeof value == "object") {
                    value = value.joint('|');
                }
                if (value != "")
                    value = "^" + value + "$";
                $(table).DataTable().column(index).search(value, true, false).draw()
            })
            $(table).find('thead .SearchInputs [inputSearch=checkbox]').change(function () {
                var index = $(this).parent().attr("index");
                var value = $(this).val();
                if (value == "Oui") {
                    value = 'checked';
                } else if (value == "Non") {
                    value = '^((?!checked).)*$';
                }
                $(table).DataTable().column(index).search(value, true, false).draw()
            })


        }
    }
    var drawCallbackTaken = false;
    var initCompleteTaken = false;
    var headerCallbackTaken = false;
    for (var k in additionnalData) {
        if (k == "drawCallback") {
            dataTableOject[k] = function (settings) {
                dataTableOject["drawCallbackTmp"](settings);
                additionnalData[k](settings)
            }
            drawCallbackTaken = true;
        }
        else if (k == "initComplete") {
            dataTableOject[k] = function (settings) {
                dataTableOject["initCompleteTmp"](settings);
                additionnalData[k](settings)
            }
            initCompleteTaken = true;
        }
        else if (k == "headerCallback") {
            dataTableOject[k] = function (nHead, aData, iStart, iEnd, aiDisplay) {
                dataTableOject["headerCallbackTmp"](nHead, aData, iStart, iEnd, aiDisplay);
                additionnalData[k](nHead, aData, iStart, iEnd, aiDisplay)
            }
            headerCallbackTaken = true;
        }
        else {
            dataTableOject[k] = additionnalData[k];
        }
    }
    if (!drawCallbackTaken) {
        dataTableOject["drawCallback"] = function (settings) {
            dataTableOject["drawCallbackTmp"](settings);
        }
    }
    if (!initCompleteTaken) {
        dataTableOject["initComplete"] = function (settings) {
            dataTableOject["initCompleteTmp"](settings);
        }
    }
    if (!headerCallbackTaken) {
        dataTableOject["headerCallback"] = function (nHead, aData, iStart, iEnd, aiDisplay) {
            dataTableOject["headerCallbackTmp"](nHead, aData, iStart, iEnd, aiDisplay);
        }
    }
    return $(table).DataTable(dataTableOject);
}


$(function () {
	$("[role='addTable']").click(function(){
	    var databaseName = $(this).attr("database");
	    $("#modal-addTable-databaseName").val(databaseName);
	    $("#modal-addTable").modal("show");
    });

    $("[role='showTable']").click(function(){
    	//console.log($(this).parent().parent().find("#dbName").html());
        var database = $(this).attr("database");
        var table = $(this).attr("table");
        window.location = "showTable?databaseName="+database+"&tableName="+table;
    });


    $("[role='removeTable']").click(function (event) {
    	event.stopPropagation();
        var database = $(this).attr("database");
        var table = $(this).attr("table");
    	console.log("removeTable");

        swal({
            title: 'Êtes-vous sûr?',
            text: "Vous allez effacer la table " + table + " de la BDD " + database + " !",
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
                    url: 'ajax/database/removeTable', // La ressource ciblée
                    type: 'POST', // Le type de la requête HTTP.
                    data: { databaseName: database, tableName: table },
                    dataType: 'json',
                    success: function (returnDat) {
                        if (returnDat == "") {
                            swal(
                              'Effacée!',
                              'La table a été effacée.',
                              'success'
                            ).then(function () {
                                location.reload();
                            });
                        } else {
                            swal('Problème', returnDat, 'error');
                        }
                    }
                });
                
            }
        })
    });
    $("#modal-addTable-form").submit(function (event) {
        event.preventDefault();
        var databaseName = $("#modal-addTable-databaseName").val();
        var tableName = $("#modal-addTable-tableName").val();
        $.ajax({
            url: 'ajax/database/addTable', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: { databaseName: databaseName, tableName: tableName },
            dataType: 'json',
            success: function (returnDat) {
                location.reload();
            }
        });
    });
    $("#modal-addTable-submit").click(function (event) {
        event.preventDefault();
        var databaseName = $("#modal-addTable-databaseName").val();
        var tableName = $("#modal-addTable-tableName").val();
        $.ajax({
            url: 'ajax/database/addTable', // La ressource ciblée
            type: 'POST', // Le type de la requête HTTP.
            data: { databaseName: databaseName, tableName: tableName },
            dataType: 'json',
            success: function (returnDat) {
                location.reload();
            }
        });
    })

});

