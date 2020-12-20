function PassivePodcast(podcastID) {

    if (confirm("Are you sure for delete this podcast?")) {
        var callParams = {
            endPoint: "/Management/Podcast/PassivePodcast",
            requestType: "POST"
        }

        var dataParams = {};
        dataParams.podcastID = podcastID;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("Podcast is passive now.");
                location.reload();
            }
            else {
                alert("Fail!");
            }
        });
    }


}

function ActivatePodcast(podcastID) {

    if (confirm("Are you sure for activate this podcast?")) {
        var callParams = {
            endPoint: "/Management/Podcast/ActivatePodcast",
            requestType: "POST"
        }

        var dataParams = {};
        dataParams.podcastID = podcastID;

        RequestAjax(callParams, dataParams, function (response) {
            debugger
            response = JSON.parse(response);

            if (response === true) {
                alert("Podcast is active now.");
                location.reload();
            }
            else {
                alert("Fail!");
            }
        });
    }


}

function EditPodcast(podcastID) {

    var callParams = {
        endPoint: "/Management/Podcast/EditPodcast",
        requestType: "POST"
    }

    var styleCss = "border:solid #ff0000 1px;";

    var dataParams = {}
    dataParams.ID = podcastID;
    dataParams.Language = $("#PodcastLanguage").val();
    dataParams.PodcastTitle = $("#PodcastTitle").val();
    dataParams.PodcastDescription = $("#PodcastDescription").val();

    var podcastSoundPath = document.getElementById("PodcastFile");
    dataParams.PodcastFilePath = podcastSoundPath.getAttribute("sound-path");



    RequestAjax(callParams, dataParams, function (response) {
        debugger
        response = JSON.parse(response);

        if (response === true) {
            alert("Podcast Edit successfull!");
        }
        else {
            alert("Fail!");
        }
    });


}