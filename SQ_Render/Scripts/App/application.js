﻿var idFull = false;
const handleFullscreen = () => {
    let main = document.body
    if (idFull) {
        if (document.exitFullscreen) {
            document.exitFullscreen()
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen()
        } else if (document.webkitCancelFullScreen) {
            document.webkitCancelFullScreen()
        } else if (document.msExitFullscreen) {
            document.msExitFullscreen()
        }
    } else {
        if (main.requestFullscreen) {
            main.requestFullscreen()
        } else if (main.mozRequestFullScreen) {
            main.mozRequestFullScreen()
        } else if (main.webkitRequestFullScreen) {
            main.webkitRequestFullScreen()
        } else if (main.msRequestFullscreen) {
            main.msRequestFullscreen()
        }
    }
    idFull = !idFull
}