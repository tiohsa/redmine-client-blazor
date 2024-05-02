function setScrollPosition(elementId, position) {
    var element = document.getElementById(elementId);
    if (element) {
        element.scrollTop = position;
    }
}
