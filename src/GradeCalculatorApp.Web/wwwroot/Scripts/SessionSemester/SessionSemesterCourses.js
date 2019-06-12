$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/SessionCourse/ReadSessionCourse",
        {sessionSemesterId: sessionSemesterId},
        true,
        readSessionCourseResponse, true);
}

function readSessionCourseResponse(data){
    resetDataTable($('#courseTable'));
    
    if (data.status) {
        data.data.courses.forEach((course, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + course.name + '</td>';
            
            row += '<td>' + course.code + '</td>';
            row += '<td>' + course.creditUnit + '</td>';
            row += '<td><a href="#" title="View" class="btn btn-primary btn-xs" onclick="viewCourseClick(\'\' + course.id + \'\')"><i class="fa fa-eye"></i></a> | <button type="button" title="Edit" class="btn btn-success btn-xs" onclick="editCourseClick(\'' + course.id + '\')"><i class="fa fa-pencil"></i></button> | <a href="#deleteCourseModal" title="Delete" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteCourseClick(\'' + course.id + '\')"><i class="fa fa-trash-o"></i></a></td>';
            row += '</tr>';

            $('#courseTable tbody').append(row);
        });

        initializeDataTable($('#courseTable'));
    }
}

// function setSessionSemesterId(sessionSemesterId) {
//     api('GET', '/SessionCourse/SetSessionSemesterId',
//         {sessionSemesterId: sessionSemesterId}, true, setSessionSemesterIdResponse, true);
// }
//
// function setSessionSemesterIdResponse(data) {
//     if (data.status) window.location = "/SessionSemester/AddCourse";
// }