function checkfile() {
    $('#importLoader').show();
    if ($("#fileUpload").val() === undefined) {
        $("#spanfile").text("Please choose a file.");
        $('#importLoader').hide();
        return false;
    }
    var file = getNameFromPath($("#fileUpload").val());
    if (file != null) {
        var extension = file.substr((file.lastIndexOf('.') + 1));
        if (extension !== 'doc' && extension !== 'docx') {
            $("#spanfile").text("You can upload only word file.");
            $('#importLoader').hide();
            return false;
        }
        $("#spanfile").text("");
        return true;
    }
    $("#spanfile").text("Please choose a file.");
    $('#importLoader').hide();
    return false;
}