function enableOmniSearch() {
    $("#id_omni_search").prop("disabled", false).removeClass("hidden");
}

function setActiveLink(id) {
    $(id).addClass("active");
}