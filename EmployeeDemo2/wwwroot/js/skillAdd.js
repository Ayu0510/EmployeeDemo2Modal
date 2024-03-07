$("#addskills").click(function () {
    $("li.list-group-item").next(".error").remove();
    let list = $("#myList");
    let value = $("#skills").val();

    let li = $("<li>", {
        "class": "list-group-item",
        "id": value
    });

    let newButton = $("<button>", {
        "text": "X",
        "type": "button",
        "id": value,
        "value": value,
        "class": "btn btn-light float-right"
    }).on('click', function () {

        removeSkills(this.value);
    });
    skillsArray.push(value);
    li.append(document.createTextNode(value));
    li.append(newButton);
    list.append(li);
    var hiddenInput = document.getElementById('SkillNames')
    hiddenInput.value = skillsArray
    $('#skills').val("");
});