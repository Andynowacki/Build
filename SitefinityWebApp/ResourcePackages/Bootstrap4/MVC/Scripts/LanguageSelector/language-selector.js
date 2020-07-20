function openLink(culture) {
    var url = $('[data-sf-role="' + culture + '"]').val();
    print("hello");
    window.location = url;
}