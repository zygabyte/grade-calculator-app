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
                        <a href="#" title="View" class="btn btn-primary btn-xs"><i class="fa fa-eye"></i></a> | 
                        <a data-toggle="modal" href="#gradeModal"  title="Grade" class="btn btn-success btn-xs" onclick="gradeStudentClick('${registeredCourse.id}')"><i class="fa fa-building-o"></i></a>
                    </td>`;
            row += '</tr>';

            $('#registeredCourseTable tbody').append(row);
        });

        initializeDataTable($('#registeredCourseTable'));
    }
}

function gradeStudentClick(registeredCourseId) {
    window.registeredCourseId = registeredCourseId;
}

function createRegisteredCourseGrade() {
    const quiz1 = $('#quiz1').val();
    const quiz2 = $('#quiz2').val();
    const assignment1 = $('#assignment1').val();
    const assignment2 = $('#assignment2').val();
    const attendance = $('#attendance').val();
    const midSemester = $('#mid_semester').val();
    const project = $('#project').val();
    const exam = $('#exam').val();
    
    const registeredCourseGrade = {
        Quiz1: quiz1, Quiz2: quiz2,
        Assignment1: assignment1, Assignment2: assignment2,
        Attendance: attendance, MidSemester: midSemester,
        Project: project, Exam: exam,
        RegisteredCourseId: window.registeredCourseId
    };

    api('POST', '/RegisteredCourseGrade/CreateRegisteredCourseGrade',
        {registeredCourseGrade: registeredCourseGrade}, true, registeredCourseGradeResponse, true);
}

function registeredCourseGradeResponse(data) {
    if (data.status) onSuccessModalHide()
}


//______________________________________UTILITIES______________________________________
function onSuccessModalHide() {
    pageLoad();
    resetField();

    $('#gradeModal').modal('hide');
}

function resetField() {
    $('#quiz1').val('');
    $('#quiz2').val('');
    $('#assignment1').val('');
    $('#assignment2').val('');
    $('#attendance').val('');
    $('#mid_semester').val('');
    $('#project').val('');
    $('#exam').val('');
}