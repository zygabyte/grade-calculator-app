function api(apiConnectType, url, data, asyncMode, callback, feedBack = false, callbackOnFailure = false) {
    $.ajax({
        type: apiConnectType,
        url: url,
        async: asyncMode,
        data: data,
        dataType: "json",
        timeout: 60000,
        headers: {
            "user-auth": $("#auth").val()
        },
        success: function (remoteData) {
            if (remoteData.status === true) {
                if (callback !== null && typeof callback === "function") {
                    if (feedBack) {
                        //swalSuccess(remoteData.Message);
                        setTimeout(function () {
                                callback(remoteData);
                            },
                            2000);
                    } else
                        callback(remoteData);
                } else {
                    if (feedBack) {
                        //swalSuccess(remoteData.Message);
                    }
                }
            } else {
                if (callbackOnFailure && callback !== null && typeof callback === "function") callback(remoteData);
                //swalWarning(remoteData.Message);
            }
        },
        error: function (e) {
            if (e.statusText === 'error' && e.responseText === '' && e.readyState === 0) { // means we had no response and the request was unsent i.e. unable to get to the server -> highly probable that it's no connectivity between client and server
                //swalNetworkError("Something seems to be wrong with your network. Please check your Internet connection");
                return;
            }
            else if (e.statusText === 'timeout' && e.readyState === 0) { // we've waited for too long oo
                //swalInfo('Could not get a response from the server. Reloading now');
                location.reload();
                return;
            }

            //swalEx();
        }
    });
}


/*
 * API Datatable Methods
 */
function initializeDataTable(table) {
    if (table.length > 0) {
        table.dataTable({ "ordering": [], "aaSorting": [] });
    }
};
function resetDataTable(table) {
    table.dataTable().fnClearTable();
    table.dataTable().fnDestroy();
};