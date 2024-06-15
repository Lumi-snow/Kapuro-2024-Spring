namespace AudioUtilizer
{
    using AudioController;
    using UnityEngine;

    public static class AudioUtilizer
    {
        public static void PlayRandomAttackSE()
        {
            int randomValue = Random.Range(1, 3);
        
            switch(randomValue)
            {
                case 1:
                    SEController.Instance.Play(SEPath.Attack01);
                    break;
                case 2:
                    SEController.Instance.Play(SEPath.Attack02);
                    break;
                case 3:
                    SEController.Instance.Play(SEPath.Attack03);
                    break;
            }
        }
    }
}