let registeredCourseId;

$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/RegisterCourse/ReadRegisteredCourses",
        {sessionSemesterId: sessionSemesterId, lecturerId: lecturerId},
        true,
        readRegisterCoursesResponse, true);
}

function readRegisterCoursesResponse(data){
    resetDataTable($('#registeredCourseTable'));
    
    console.log('data ');
    console.log(data);

    if (data.status) {
        courses = data.data;
        data.data.forEach((registeredCourse, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + registeredCourse.course + '</td>';
            row += '<td>' + registeredCourse.courseCode + '</td>';
            row += '<td>' + registeredCourse.courseCredit + '</td>';
            row += '<td>' + registeredCourse.student + '</td>';
            row += `<td>
                        <a href="#" title="View" class="btn btn-primary btn-xs" onclick="viewLecturerClick(\'\' + lecturer.id + \'\')"><i class="fa fa-eye"></i></a> | 
                        <a data-toggle="modal" href="#gradeModal"  title="Grade" class="btn btn-success btn-xs" onclick="gradeStudentClick(\'' + registeredCourse.id + '\')"><i class="fa fa-building-o"></i></a>
                    </td>`;
            row += '</tr>';

            $('#registeredCourseTable tbody').append(row);
        });

        initializeDataTable($('#registeredCourseTable'));
    }
}

function gradeStudentClick(registeredCourseId) {
    console.log('registered course id is');
    console.log(registeredCourseId);
    window.registeredCourseId = registeredCourseId;
}