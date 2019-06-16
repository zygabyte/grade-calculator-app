let courseIds = [];
let courses = [];
let registeredCourses = [];

$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/RegistrationCourse/ReadRegistrationCourses",
        {sessionSemesterId: sessionSemesterId, programmeId: 1},
        true,
        readRegisterCoursesResponse, true);
}

function readRegisterCoursesResponse(data){
    resetDataTable($('#registerCourseTable'));

    if (data.status) {
        courses = data.data;
        data.data.forEach((course, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + course.course + '</td>';
            row += '<td>' + course.courseUnit + '</td>';
            row += '<td>' + course.lecturer + '</td>';
            row += `<td><div class="custom-control custom-checkbox">
                <input type="checkbox" class="custom-control-input" id=${course.id}>
                <label class="custom-control-label" for=${course.id}></label>
                </div></td>`;
            row += '</tr>';

            $('#registerCourseTable tbody').append(row);
        });

        initializeDataTable($('#registerCourseTable'));
    }
}

function registerCourses(){
    courseIds = [];

    $("input:checkbox").each(function(){
        let $this = $(this);

        if($this.is(":checked")){
            courseIds.push(parseInt($this.attr("id")));
        }
    });
    
    console.log('course ids');
    console.log(courseIds);
    
    courses.forEach(course =>{
       if (courseIds.includes(course.id)) registeredCourses.push({StudentId: studentId, CourseId: course.courseId, LecturerId: course.lecturerId});
    });
    
    console.log('registered courses');
    console.log(registeredCourses);

    api("POST",
        "/RegisterCourse/CreateRegisterCourse",
        {registeredCourses: registeredCourses},
        true,
        registeredCoursesResponse, true);
}

function registeredCoursesResponse(data) {
    if (data.status) window.location = "/";
}