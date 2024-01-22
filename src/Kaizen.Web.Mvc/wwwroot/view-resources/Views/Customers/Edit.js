(function ($) {
    var _userService = abp.services.app.user,
        l = abp.localization.getSource('Kaizen'),
        firstLoads = { 'Addresses': false }

    const userId = $('#User_Id').val();

    var address = new Address();
    $('a[data-bs-toggle="tab"]').on("click", function () {
        switch ($(this).find("span").text()) {
            case "Addresses":
                if (firstLoads.Addresses === false) {
                    address.setupGrid(userId);
                    firstLoads.Addresses = true;
                }

                break;
        }
    });
})(jQuery);
