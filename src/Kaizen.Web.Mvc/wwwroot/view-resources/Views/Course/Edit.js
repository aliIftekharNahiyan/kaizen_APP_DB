(function ($) {
    var _courseService = abp.services.app.course,
        l = abp.localization.getSource('Kaizen'),
        firstLoads = {
            'Terms': false
        }   
        
    const courseId = $('#Course_Id').val();
    const entityType = $('#EntityType').val();

    $('a[data-bs-toggle="tab"]').on("click", function () {
        console.log($(this).attr("href"));

        switch ($(this).attr("href")) { 
            case "#edit-course-terms":
                if (firstLoads.Terms === false) {
                    var terms = new CourseTerm(courseId);

                    terms.setupGrid();
                    firstLoads.Terms = true;
                }

                break;
        }
    });
})(jQuery);
