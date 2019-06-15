let programmeCourseId;

$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    api("GET",
        "/ProgrammeCourse/ReadProgrammeCourse",
        {programmeId: programmeId},
        true,
        readProgrammeCourseResponse, true);
}

function readProgrammeCourseResponse(data){
    resetDataTable($('#courseTable'));

    if (data.status) {
        data.data.forEach((course, i) => {
            let row = '<tr>';
            row += '<td>' + (i + 1) + '</td>';
            row += '<td>' + course.name + '</td>';
            row += '<td>' + course.code + '</td>';
            row += '<td>' + course.creditUnit + '</td>';
            row += `<td><a href="#deleteProgrammeCourseModal" title="Remove" data-toggle="modal" class="btn btn-danger btn-xs" onclick="deleteProgrammeCourseClick('${course.id}')"><i class="fa fa-trash-o"></i></a></td>`;
            row += '</tr>';

            $('#courseTable tbody').append(row);
        });

        initializeDataTable($('#courseTable'));
    }
}

function deleteProgrammeCourseClick(courseId) {
    programmeCourseId = courseId;
}

function deleteProgrammeCourse() {
    api('GET', '/ProgrammeCourse/DeleteProgrammeCourse',
        {programmeId: programmeId, courseId: programmeCourseId}, true, deleteProgrammeCourseResponse, true);
}

function deleteProgrammeCourseResponse(data) {
    if (data.status) onSuccess();
}


//______________________________________UTILITIES______________________________________
function onSuccess() {
    pageLoad();

    $('#deleteProgrammeCourseModal').modal('hide');
}