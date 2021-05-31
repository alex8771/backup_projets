//appends an "active" class to .popup and .popup-content when the "Open" button is clicked
$(".openRole").on("click", function () {
    $(".popup-overlay-role, .popup-content-role").addClass("active");
});

//removes the "active" class to .popup and .popup-content when the "Close" button is clicked 
$(".closeRole").on("click", function () {
    $(".popup-overlay-role, .popup-content-role").removeClass("active");
});

//appends an "active" class to .popup and .popup-content when the "Open" button is clicked
$(".openDirecteur").on("click", function () {
    $(".popup-overlay-directeur, .popup-content-directeur").addClass("active");
});

//removes the "active" class to .popup and .popup-content when the "Close" button is clicked 
$(".closeDirecteur").on("click", function () {
    $(".popup-overlay-directeur, .popup-content-directeur").removeClass("active");
});

