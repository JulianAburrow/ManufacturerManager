namespace MMBusinessLayer.Models.TogetherAI;

public class TogetherChatRequest
{
    public string Model { get; set; } = string.Empty;

    public List<TogetherMessage> Messages { get; set; } = [];

    public double Temperature { get; set; } = 0.2;
}