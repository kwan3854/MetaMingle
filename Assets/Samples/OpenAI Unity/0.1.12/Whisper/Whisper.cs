using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Michsky.MUIP;

namespace OpenAI
{
    public class Whisper : MonoBehaviour
    {
        [SerializeField] private ButtonManager recordButton;
        [SerializeField] private Dropdown dropdown;
        [SerializeField] private ChatTest chatTest;
        [SerializeField] private Image progress;

        private const string FileName = "output.wav";
        private const int RecordDuration = 5;
        private OpenAIApi openai = new OpenAIApi();
        private AudioClip clip;
        private bool isRecording;
        private float time;

        private void Start()
        {
            InitDropdownOptions();
            BindEventListeners();

            int savedMicDeviceIndex = PlayerPrefs.GetInt("user-mic-device-index");
            dropdown.SetValueWithoutNotify(savedMicDeviceIndex);


        }

        private void InitDropdownOptions()
        {
            foreach (var device in Microphone.devices)
            {
                dropdown.options.Add(new Dropdown.OptionData(device));
            }
        }

        private void BindEventListeners()
        {
            recordButton.onClick.AddListener(ToggleRecording);
            dropdown.onValueChanged.AddListener(ChangeMicrophone);
        }

        public void ChangeMicrophone(int index)
        {
            PlayerPrefs.SetInt("user-mic-device-index", index);
        }

        private async void ToggleRecording()
        {
            if (isRecording)
            {
                StopRecordingAndTranscribe();
            }
            else
            {
                StartRecording();
            }
        }

        private void StartRecording()
        {
            Debug.Log("Start recording...");
            isRecording = true;

            var index = PlayerPrefs.GetInt("user-mic-device-index");

            // Check if the saved index is within the range of available microphone devices
            if (index >= 0 && index < Microphone.devices.Length)
            {
                var selectedMicDevice = dropdown.options[index].text;
                clip = Microphone.Start(selectedMicDevice, false, RecordDuration, 44100);
            }
            else if (index == -1)
            {
                Debug.Log("Please select a microphone device from the dropdown.");
            }
            else
            {
                Debug.LogWarning("Invalid microphone device index: " + index);
            }
        }

        private async void StopRecordingAndTranscribe()
        {
            isRecording = false;
            Debug.Log("Stop recording...");
            Microphone.End(null);

            byte[] data = SaveWav.Save(FileName, clip); // data== null

            var req = new CreateAudioTranscriptionsRequest
            {
                FileData = new FileData() { Data = data, Name = "audio.wav" },
                Model = "whisper-1",
                Language = "ko"
            };
            var res = await openai.CreateAudioTranscription(req);

            chatTest.SendReply(res.Text);
        }

        private void Update()
        {
            if (isRecording)
            {
                UpdateProgress();
            }
        }

        private void UpdateProgress()
        {
            time += Time.deltaTime;
            progress.fillAmount = time / RecordDuration;

            if (time >= RecordDuration)
            {
                ResetProgress();
                StopRecordingAndTranscribe();
            }
        }

        private void ResetProgress()
        {
            time = 0;
            progress.fillAmount = 0;
        }
    }
}