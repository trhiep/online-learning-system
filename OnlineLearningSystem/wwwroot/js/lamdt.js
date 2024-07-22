
$(document).ready(function () {

    $('#studenListTbl').DataTable({
        "processing": true,
        "serverSide": false,
        "ajax": {
            "url": "/ClassStudents/StudentList?handler=FetchStudent",
            "type": "GET",
        },
        "columns": [
            { "data": "username" },
            { "data": "fullname" },
            { "data": "email" }
        ]
    });
});


