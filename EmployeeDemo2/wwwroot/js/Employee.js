$(document).ready(function () {
    debugger
    declarationFields();
    let employeeClassname = "Add-employee";
    // messageShow();
    $("#saveEmployee").click(function () {
        debugger
        let isValid = Validate(employeeClassname);
        // let isValid = true;
        if (isValid == true) {
            if (!isEdit) {
                const employeedatajson = {
                    id: generateId(),
                    firstname: firstname.val(),
                    lastname: lastname.val(),
                    email: email.val(),
                    gender: gender.val(),
                    DateOfBirth: DateOfBirth.val(),
                    DateOfJoining: DateOfJoining.val(),
                    designation: designation.val(),
                    image: output.src,
                    skills: skills,
                    details: details.val()
                };

                employeeSweetAlert(employeedatajson, addEmployee);
            }
            else {
                const employeedatajson = {
                    id: editId,
                    firstname: firstname.val(),
                    lastname: lastname.val(),
                    email: email.val(),
                    gender: gender.val(),
                    DateOfBirth: DateOfBirth.val(),
                    DateOfJoining: DateOfJoining.val(),
                    designation: designation.val(),
                    image: output.src,
                    skills: skills,
                    details: details.val()
                };

                employeeEditSweetAlert(editId, employeedatajson, editEmployee)
            }
        }
    })
    
});

function declarationFields() {
    form = $("#myForm");
    modelTitle = $('#modelTitle');
    firstname = $("#firstname");
    lastname = $("#lastname");
    email = $("#email");
    gender = $(".gender");
    $('input[name="gender"]').on("click", function () {
        gender = $('input[name = "gender"]:checked');
    });
    DateOfBirth = $("#DateOfBirth");
    DateOfJoining = $("#DateOfJoining");
    designation = $("#designation");
    details = $("#details");
    isEdit = false;
    editId = null;

    return form, modelTitle, firstname, lastname, email, gender, DateOfBirth, DateOfJoining, designation, image, search, skills, details, isEdit, editId;
}
declarationFields();
