using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public SelectionManager selectionManager;
    public GameManager gameManager;
    public TutorialManager tutorialManager;

    private string output;
    public float typeSpeed;
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;

    public AudioSource npcAudio;
    public AudioClip npcSound;

    IEnumerator Type()
    {
        dialogueText.text = "";
        foreach (char letter in output)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        if (tutorialManager.isFirstRound())
        {
            tutorialManager.ShowStories();
        }
        else
        {
            selectionManager.makeSelection();
        }
    }

    IEnumerator Feedback()
    {
        dialogueText.text = "";
        npcAudio.PlayOneShot(npcSound, 1.0f);
        foreach (char letter in output)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        yield return new WaitForSeconds(1.5f);
        dialoguePanel.SetActive(false);
    }

    // Will sync up correct treatment w/ corresponding trio of stories
    private string currentCorrect;

    public string getCondition()
    {
        return currentCorrect;
    }

    public void abortDialogue()
    {
        dialoguePanel.SetActive(false);
    }


    private string[] treatmentOptions = new string[] {"Stitches", "Extraction", "Bone Setting", "Amputation", "Cauterization", 
                                                       "Medicinal Herbs", "Ointment", "Disinfectant", "Leeches", "Maggots",
                                                       "Rest", "Bandage", "Incense"};

    private string[] positiveFeedback = new string[] { "Great idea, you really know your stuff.", "Ooh, why didn't I think of that?", "That sounds right, thank you!", "Perfect, you're the best!", "That'll do the trick, good thinking." };
    private string[] negativeFeedback = new string[] { "Really? I hope you're right...", "Well, you're the expert, I guess...", "I don't know about this...", "Uhhh, are you sure that'll work...?", "Sure, what's the worst that could happen?" };

    // ALL DIALOGUE - START ------------------------------------------------------------------------------------------------------------

    // Let Ben handle this mess...
    // Replace each element soldiers' story dialogue, see data sheet for some ideas
    private string[,] allStories = {
        {"One of those brutes got me with an axe. It’s a deep cut, probably won’t heal on its own.", "Hey, I might’ve tripped and fallen… on my own dagger. I need something to close the wound.", "A brawl broke out in the mess hall again, got a busted lip."},
        {"There’s an arrow lodged in my back, care to yank it out for me?", "Dinner got rowdy last night. Yes, that’s a fork stuck in my shoulder.", "One of our archers got me to try whittling… didn’t warn me about the splinters."},
        {"Some jerk shoved me down the hill during training, felt my arm break when I hit the ground.", "An opponent struck my leg with a mace, snapped it like a twig… don’t ask how I walked in here.", "I blocked a heavy blow a while back, my arm has been stiff and crooked ever since."},
        {"They nailed me with a trebuchet, just my luck… crushed my leg flat, I can’t even move it anymore.", "I tried feeding a stray dog, didn’t go well. It mutt mauled my hand badly, I’m surprised it’s still attached…", "I was kicked off a cliff yesterday, landed on my now shattered legs. It took two men just to drag me in here."},
        {"Oh god, I’m bleeding out. Do something, quickly!", "An enemy slashed me with a broken sword. It was all jagged, the gash won’t stop bleeding.", "I, uh, have hemorrhoids… you got anything for that?"},
        {"It’s so cold, the walls spinning… I’ve felt worse and worse ever since that rat bit me in the storeroom.", "I feel sick, vomited an hour ago. Maybe eating that fuzzy cheese was a bad idea.", "I’ve got this splitting headache, I can’t fight nor sleep like this. Can you help?"},
        {"They dumped boiling oil on me, who does that? So wasteful… burned me head to toe…", "We had to retreat through some tall grass today, now I’m covered in red bumps. Some bugs must’ve gotten into my armor…", "Had an accident on cooking duty today, burned my hand badly and ruined the soup..."},
        {"Some thug jabbed me with the rustiest dagger I’ve ever seen. Should probably clean out the wound.", "There’s a small cut on my ankle. I’m sure it’d be fine if we hadn’t marched through a filthy swamp afterward…", "Another medic treated my wounds a while ago, but they’re not healing up. I don’t think he ever washed his hands…"},
        {"I was bashed with a shield the other day, left a serious bruise which isn’t going away.", "I had a finger reattached recently, the surgery went great but it’s all numb, like it’s not getting enough blood.", "I’ve been standing out on guard duty for so long… my legs are all swollen and discolored…"},
        {"I’ve left this wound unattended for too long, it’s started to blacken and rot.", "Looks like I’ve got some kind of ulcer on my foot, maybe I need new boots…", "Last night’s battle was awful. Bitter cold, sharp winds, and rain on top of it all. Thankfully the worst injury I got was frostbite."},
        {"I’ve been stationed on the front lines for weeks… I can barely stand at this point, let alone fight.", "I lost my footing during training, twisted my ankle and tumbled. I don’t think it’s broken but it’s painful to walk.", "I… uh… hit in the… um… head hurt…"},
        //No row for Prayer, random chance of success w/ any condition
        {"That last battle was hectic, left me with too many bruises and wounds to count. None too deep, thankfully, I just need them covered.", "Had to train with a drunk guy, he thought it’d be fine to use a real weapon. He was terrible, but still gave me some shallow cuts.", "Somebody startled me while I was sifting through the armory, led to me getting this cut on my hand."},
        {"All this violence is starting to get to me, I can’t focus or sleep. Do you have anything to calm my nerves?", "I can’t do this anymore, I’m just too stressed. It’s getting harder to breathe… my hands won’t stop shaking…", "Hey, I’m just looking around. It smells terrible in here, you should do something about that."},
        //No row for Bloodletting, always fails

    };

    // ALL STORIES - END --------------------------------------------------------------------------------------------------------------
    
    public void promptUser()
    {

        int randomIndex = Random.Range(0, 13);
        currentCorrect = treatmentOptions[randomIndex];
        output = allStories[randomIndex, Random.Range(0, 3)];


        dialoguePanel.SetActive(true);
        StartCoroutine(Type());

    }

    public void provideFeedback(bool wasCorrect)
    {
        if (wasCorrect)
            output = positiveFeedback[Random.Range(0, 5)];
        else
            output = negativeFeedback[Random.Range(0, 5)];
        StartCoroutine(Feedback());
    }

    // Start is called before the first frame update
    void Start()
    {
        // Place after start & walk in
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
