(function ($) {
    var _universityService = abp.services.app.university,
        l = abp.localization.getSource('Kaizen'),
        firstLoads = {
            'Courses': false
        }   
        
    const universityId = $('#University_Id').val();
    const entityType = $('#EntityType').val();

    $('a[data-bs-toggle="tab"]').on("click", function () {
        console.log($(this).attr("href"));

        switch ($(this).attr("href")) { 
            case "#edit-university-courses":
                if (firstLoads.Courses === false) {
                    var courses = new Course(universityId);

                    courses.setupGrid();
                    firstLoads.Courses = true;
                }

                break;
        }
    });
})(jQuery);
