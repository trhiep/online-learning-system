function chooseAll() {
    var btnAll = document.getElementById("all");
    if (btnAll.checked) {
        var listclassroom = document.getElementsByClassName("classroom");
        for (var i = 0; i < listclassroom.length; i++) {
            listclassroom[i].checked = true;
        }
        var listcbx = document.getElementsByClassName("student");
        for (var i = 0; i < listcbx.length; i++) {
            listcbx[i].checked = true;
        }
    } else {
        var listclassroom = document.getElementsByClassName("classroom");
        for (var i = 0; i < listclassroom.length; i++) {
            listclassroom[i].checked = false;
        }
        var listcbx = document.getElementsByClassName("student");
        for (var i = 0; i < listcbx.length; i++) {
            listcbx[i].checked = false;
        }
    }
}
function chooseAllIn(no) {
    console.log(no);
    var item = document.getElementById("class-" + no);
    if (item.checked) {
        var listStudent = document.getElementsByClassName(no);
        for (var i = 0; i < listStudent.length; i++) {
            listStudent[i].checked = true;
        }
    } else {
        var listStudent = document.getElementsByClassName(no);
        for (var i = 0; i < listStudent.length; i++) {
            listStudent[i].checked = false;
        }
    }
}
function change(id) {
    var item = document.getElementById(id);
    var split = item.split;
    if (!item.checked) {
        var parent = document.getElementById("class-" + split[0]);
        parent.checked = false;
        var all = document.getElementById("all");
        all.checked = false;
    }
}
function toggleDisplayListClass() {
    var listClass = document.getElementById("list-class");
    if (listClass.style.display == 'block') {
        listClass.style.display = 'none';
    } else {
        listClass.style.display = 'block';
    }
}
function toggleDisplayListStudent(no) {
    var listStudent = document.getElementById("list-student-" + no);
    if (listStudent.style.display == 'block') {
        listStudent.style.display = 'none';
    } else {
        listStudent.style.display = 'block';
    }
}