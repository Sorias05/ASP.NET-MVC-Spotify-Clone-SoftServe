﻿var player = document.getElementById('player');
player.currentTime = localStorage.getItem('time');
player.volume = localStorage.getItem('volume');

player.addEventListener('ended', function () {
    document.getElementById('btnNext').click();
});

window.addEventListener("beforeunload", function () {
    localStorage.setItem('time', player.currentTime);
    localStorage.setItem('volume', player.volume);
});

function initProgressBar() {
    var length = player.duration
    var current_time = player.currentTime;

    var totalLength = calculateTotalValue(length)
    document.getElementById("end-time").innerHTML = totalLength;

    var currentTime = calculateCurrentValue(current_time);
    document.getElementById("start-time").innerHTML = currentTime;

    var progressbar = document.getElementById('seek-obj');
    progressbar.value = (player.currentTime / player.duration);
    progressbar.addEventListener("click", seek);

    function seek(event) {
        var percent = event.offsetX / this.offsetWidth;
        player.currentTime = percent * player.duration;
        progressbar.value = percent / 100;
    }
};

function calculateTotalValue(length) {
    var minutes = Math.floor(length / 60),
        seconds_int = length - minutes * 60,
        seconds_str = seconds_int.toString(),
        seconds = seconds_str.substr(0, 2),
        time = minutes + ':' + seconds

    return time;
}

function calculateCurrentValue(currentTime) {
    var current_hour = parseInt(currentTime / 3600) % 24,
        current_minute = parseInt(currentTime / 60) % 60,
        current_seconds_long = currentTime % 60,
        current_seconds = current_seconds_long.toFixed(),
        current_time = (current_minute < 10 ? "0" + current_minute : current_minute) + ":" + (current_seconds < 10 ? "0" + current_seconds : current_seconds);

    return current_time;
}