window.speech = {
    start: function () {
        const recognition = new (window.SpeechRecognition || window.webkitSpeechRecognition)();
        recognition.lang = "en-GB";
        recognition.interimResults = false;
        recognition.maxAlternatives = 1;

        DotNet.invokeMethodAsync('MMUserInterface', 'SetRecordingState', true);

        recognition.onresult = function (event) {
            const text = event.results[0][0].transcript;
            DotNet.invokeMethodAsync('MMUserInterface', 'ReceiveAudioFromJs', text);
        };

        recognition.onend = function () {
            DotNet.invokeMethodAsync('MMUserInterface', 'SetRecordingState', false);
        };

        recognition.start();
    }
};
