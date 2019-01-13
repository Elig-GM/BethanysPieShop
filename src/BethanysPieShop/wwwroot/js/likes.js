const ROOT_API_PIEDATA = "/api/PieData/";

function likeToReview(pieReviewID) {
    $.ajax({
        method: "POST",
        url: ROOT_API_PIEDATA + pieReviewID,
        success: function () {
            $("like").empty();
            document.getElementById(`likeReview-${pieReviewID}`).innerHTML = '<i class="far fa-heart"></i> Dislike';
        },
        error: function (msg) {
            // toastr.error('An error occured while trying to save the user.');
        }
    });
}
{/* <i class="far fa-heart"></i> */}