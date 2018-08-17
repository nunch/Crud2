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

function downloadURI(uri) {
    get(uri, {});
}


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
        if (typeof str == "string")
            return str.replace(new RegExp(find, 'g'), replace);
        else
            return str;
    } catch (e) {
        return str;
    }
}
function formatDateFromSQL(date) {
    return moment(date, 'YYYY-MM-DDTHH:mm:ss').format("DD/MM/YYYY")
}
function formatDateTimeFromSQL(date) {
    return moment(date, 'YYYY-MM-DDTHH:mm:ss').format("DD/MM/YYYY HH:mm")
}
function setCustomInputSearch(table, input) {
    $(input).keyup(function () {
        $(table).DataTable().search($(this).val()).draw();
    });
    if ($(input).parent().find("SearchIcon").length == 0) {
        $(input).parent().append('<i class="fa fa-search SearchIcon"></i>');
    }
    if ($(input).parent().prop("tagName") == "LABEL") {
        if (!$(input).parent().parent().hasClass("customFilter")) {
            $(input).parent().wrap("<div class='customFilter'></div>")
        }
    } else {
        $(input).wrap("<label></label>");
        $(input).parent().wrap("<div class='customFilter'></div>")
    }
    $(input).focus(function () {
        $(this).parent().find(".SearchIcon").addClass("Active")
    })
    $(input).blur(function () {
        ($(this).val() == "") ? $(this).parent().find(".SearchIcon").removeClass("Active") : '';
    })
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
    var tableId = $(table).attr("id");
    var keys = Object.keys(dataTableInitSelectDataObjectsActiveSearch[tableId])
    for (var i = 0; i < keys.length; i++) {
        dataTableInitSelectDataObjectsActiveSearch[tableId][keys[i]] = "";
    }
    $(table).find('thead .SearchInputs [inputSearch]').val("");
    $(table).DataTable().search('').columns().search('').draw();
}
function initDataTable(table, columns, order, additionnalData) {
    var tableId = $(table).attr("id");
    dataTableInitSelectDataObjectsInitDataTable[tableId] = {}
    dataTableInitSelectDataObjectsActiveSearch[tableId] = {}
    //$(table).css("width", "100%")
    jQuery.extend(jQuery.fn.dataTableExt.oSort, {
        "date-fr-time-pre": function (a) {
            a = a.replace(" ", "/");
            a = a.replace(":", "/");
            var ukDatea = a.split('/');
            return (ukDatea[2] + ukDatea[1] + ukDatea[0] + ukDatea[3] + ukDatea[4]) * 1;
        },

        "date-fr-time-asc": function (a, b) {
            return ((a < b) ? -1 : ((a > b) ? 1 : 0));
        },

        "date-fr-time-desc": function (a, b) {
            return ((a < b) ? 1 : ((a > b) ? -1 : 0));
        },

        "date-fr-pre": function (a) {
            var ukDatea = a.split('/');
            return (ukDatea[2] + ukDatea[1] + ukDatea[0]) * 1;
        },

        "date-fr-asc": function (a, b) {
            return ((a < b) ? -1 : ((a > b) ? 1 : 0));
        },

        "date-fr-desc": function (a, b) {
            return ((a < b) ? 1 : ((a > b) ? -1 : 0));
        }
    });
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
                            dataTableInitSelectDataObjectsInitDataTable[tableId][tmp[i]] = [""];
                            var rowData = $(table).DataTable().columns(tmp[i], { search: "applied" }).data()[0];
                            if ($(table).find(".SearchInputs [index=" + tmp[i] + "]") == undefined) {
                                dataTableInitSelectDataObjectsActiveSearch[tmp[i]] = "";
                                for (var j = 0; j < rowData.length; j++) {
                                    if (!contains(dataTableInitSelectDataObjectsInitDataTable[tableId][tmp[i]], rowData[j])) {
                                        dataTableInitSelectDataObjectsInitDataTable[tableId][tmp[i]].push(rowData[j]);
                                    }
                                }
                                dataTableInitSelectDataObjectsInitDataTable[tableId][tmp[i]].sort();
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
                                $(table).find(".SearchInputs [index=" + tmp[i] + "] select").val(dataTableInitSelectDataObjectsActiveSearch[tableId][tmp[i]]);
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
                            if (dataTableInitSelectDataObjectsInitDataTable[tableId][tmp[i]] != undefined) {
                                for (var j = 0; j < dataTableInitSelectDataObjectsInitDataTable[tableId][tmp[i]].length; j++) {
                                    var value = dataTableInitSelectDataObjectsInitDataTable[tableId][tmp[i]][j];
                                    var valueReplace = replaceAll(value, '"', '\\"');
                                    options += '<option value="' + valueReplace + '">' + value + '</option>';
                                }
                            }
                            html = '<th index="' + tmp[i] + '" style="padding: 2px;"><select inputSearch="select" style="margin-bottom: 2px;"><option value=""></option>' + options + '</select></th>';
                        } else {
                            for (var key in values) {
                                var keyReplace = replaceAll(key, '"', '\\"');
                                options += '<option value="' + keyReplace + '">' + values[key] + '</option>';
                            }
                            html = '<th index="' + tmp[i] + '" style="padding: 2px;"><select inputSearch="select" style="margin-bottom: 2px;"><option value=""></option>' + options + '</select></th>';
                        }
                    }
                    else if (columnToSearch[tmp[i]].input == "text") {
                        html = '<th index="' + tmp[i] + '" style="padding: 2px;"><input inputSearch="text" type="text" placeholder="Rechercher..."></th>';
                    }
                    else if (columnToSearch[tmp[i]].input == "checkbox") {
                        html = '<th index="' + tmp[i] + '" style="padding: 2px;"><select inputSearch="checkbox" style="margin-bottom: 2px;"><option value=""></option><option value="Oui">Oui</option><option value="Non">Non</option></select></th>';
                    }
                }
                $(table).find('thead .SearchInputs').append(html);

            }
            // Aucun input, on enlève la ligne et on fait un gros header
            if ($(table).find('thead .SearchInputs [inputSearch]').length == 0) {
                $(table).find('thead .SearchInputs').remove();
                $(table).find('thead tr').addClass("bigTr");
            }
            $(table).find('thead .SearchInputs [inputSearch=text]').on("keyup", function () {
                var index = $(this).parent().attr("index");
                var value = $(this).val();
                $(table).DataTable().column(index).search(value, true, false).draw()
            })
            $(table).find('thead .SearchInputs [inputSearch=select]').change(function () {
                var index = $(this).parent().attr("index");
                var value = $(this).val();
                dataTableInitSelectDataObjectsActiveSearch[tableId][index] = value;
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
                additionnalData["drawCallback"](settings)
            }
            drawCallbackTaken = true;
        }
        else if (k == "initComplete") {
            dataTableOject[k] = function (settings) {
                dataTableOject["initCompleteTmp"](settings);
                additionnalData["initComplete"](settings)
            }
            initCompleteTaken = true;
        }
        else if (k == "headerCallback") {
            dataTableOject[k] = function (nHead, aData, iStart, iEnd, aiDisplay) {
                dataTableOject["headerCallbackTmp"](nHead, aData, iStart, iEnd, aiDisplay);
                additionnalData["headerCallback"](nHead, aData, iStart, iEnd, aiDisplay)
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

function post(path, params) {
    open(path, params, "post");
}


function get(path, params) {
    open(path, params, "get");
}


function open(path, params, method) {
    method = method || "post"; // Set method to post by default if not specified.

    // The rest of this code assumes you are not using a library.
    // It can be made less wordy if you use one.
    var form = document.createElement("form");
    form.setAttribute("method", method);
    form.setAttribute("action", path);
    form.setAttribute("target", "_blank");

    for (var key in params) {
        if (params.hasOwnProperty(key)) {
            if (typeof params[key] == "object") {
                for (var paramsKey in params[key]) {
                    if (params[key].hasOwnProperty(paramsKey)) {
                        if (typeof params[key][paramsKey] == "object") {
                            for (var paramsKeyKey in params[key][paramsKey]) {
                                if (params[key][paramsKey].hasOwnProperty(paramsKeyKey)) {
                                    var hiddenField = document.createElement("input");
                                    hiddenField.setAttribute("type", "hidden");
                                    hiddenField.setAttribute("name", key + "[" + paramsKey + "][" + paramsKeyKey + "]");
                                    hiddenField.setAttribute("value", params[key][paramsKey][paramsKeyKey]);

                                    form.appendChild(hiddenField);
                                }
                            }
                        } else {
                            var hiddenField = document.createElement("input");
                            hiddenField.setAttribute("type", "hidden");
                            hiddenField.setAttribute("name", key + "[" + paramsKey + "]");
                            hiddenField.setAttribute("value", params[key][paramsKey]);

                            form.appendChild(hiddenField);
                        }
                    }
                }
            } else {
                var hiddenField = document.createElement("input");
                hiddenField.setAttribute("type", "hidden");
                hiddenField.setAttribute("name", key);
                hiddenField.setAttribute("value", params[key]);

                form.appendChild(hiddenField);
            }
        }
    }

    document.body.appendChild(form);
    form.submit();
}

// Fonction d'affichage du loader --------------------------------------------------------------
function showLoader() {
    $('.loaderContainer').removeClass('hidden');
}

// Fonction de masquage du loader --------------------------------------------------------------
function hideLoader() {
    $('.loaderContainer').addClass('hidden');
}


// READY FUNCTION ===============================================================================
$(document).ready(function () {
    /*
    // Administration nav display ---------------------------------------------------------------
    $("#AdministrationAccess").click(function () {
        $("#AdministrationNav").toggleClass("Active");
        $("#AdministrationAccess").toggleClass("Active");
    });
    $(window).resize(function () {
        $(".sorting").width("");    //This is the class it sets to each < th >
        $(".dataTable").width("100%");     //Class I set to the table for  this. An id won't work.
        $(".dataTables_scrollHeadInner").width("100%"); // Class of div added by DataTables, contains the header table.
    });*/
    hideLoader();
});
