function showModalDialog(dialog) {
    dialog.showModal();
}

function showModelessDialog(dialog) {
    dialog.show();
}

function closeDialog(dialog) {
    dialog.close();
}

function pointerCapture(element, pointerId) {
    element.setPointerCapture(pointerId);
}

function pointerRelease(element, pointerId) {
    element.releasePointerCapture(pointerId);
}

const popupWindows = new Map();  // list of currently opened popups. Key is url, value is window object

function showPopupWindow(url, windowFeatures) {
    cleanupClosedWindows();

    var w = popupWindows.get(url);

    if (w == undefined) {
        w = window.open(url, "_blank", windowFeatures);  // open new window
        popupWindows.set(url, w);
    }
    else
        w.focus();  // show existing window

    var win = DotNet.createJSObjectReference(w);
    return win;
}

function cleanupClosedWindows() {  // remove closed windows from the popupWindows map
    popupWindows.forEach((value, key) => {
        var url = key;
        var window = value;
        if (window.closed)
            popupWindows.delete(url);
    })
}

function closePopupWindow(url) {
    var w = popupWindows.get(url);
    if (w == undefined) return;

    w.close();
    popupWindows.delete(url);
}
