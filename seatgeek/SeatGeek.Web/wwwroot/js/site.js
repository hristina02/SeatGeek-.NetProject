function statistics() {
    $('#statistics_btn').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();

        // hasClass('d-none') -> Statistics are hidden
        if ($('#statistics_box').hasClass('d-none')) {
            $.get('https://localhost:7015/api/statistics', function (data) {
                $('#total_events').text(data.totalEvents + " Events");
                $('#total_tickets').text(data.totalTickets + " Tickets");

                $('#statistics_box').removeClass('d-none');

                $('#statistics_btn').text('Hide Statistics');
                $('#statistics_btn').removeClass('btn-primary');
                $('#statistics_btn').addClass('btn-danger');
            });
        } else {
            $('#statistics_box').addClass('d-none');

            $('#statistics_btn').text('Show Statistics');
            $('#statistics_btn').removeClass('btn-danger');
            $('#statistics_btn').addClass('btn-primary');
        }
    });
}


window.onload = function () {
    var canvas = document.getElementById('picassoCanvas');
    if (canvas.getContext) {
        var ctx = canvas.getContext('2d');

        // Set canvas size
        canvas.width = 200;
        canvas.height = 200;

        // Draw abstract shapes
        ctx.fillStyle = "#FFD700"; // Gold
        ctx.beginPath();
        ctx.moveTo(100, 0);
        ctx.lineTo(200, 100);
        ctx.lineTo(100, 200);
        ctx.lineTo(0, 100);
        ctx.closePath();
        ctx.fill();

        ctx.fillStyle = "#AFEEEE"; // PaleTurquoise
        ctx.beginPath();
        ctx.arc(100, 100, 50, 0, Math.PI * 2, true); // Circle
        ctx.fill();

        // Add some dynamic lines for visual interest
        ctx.moveTo(0, 0);
        ctx.lineTo(200, 200);
        ctx.moveTo(200, 0);
        ctx.lineTo(0, 200);
        ctx.strokeStyle = "#FFFFFF";
        ctx.stroke();
    }
};

