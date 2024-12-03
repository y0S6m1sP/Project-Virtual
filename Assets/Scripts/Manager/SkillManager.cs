using UnityEngine;

public class SkillManager : MonoBehaviour
{

    public static SkillManager instance;

    public DodgeSkill Dodge { get; private set; }
    public ParrySkill Parry { get; private set; }


    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Dodge = GetComponent<DodgeSkill>();
        Parry = GetComponent<ParrySkill>();
    }

}
