using System;
using UnityEngine;
using UnityEngine.Assertions;

//�W�F�l���b�N�ł���TYpe��MonoBehaviour���p�������N���X���w��
//IDisposable��interface
public abstract class SingletonWithMonoBehaviour<TYpe> : MonoBehaviour, IDisposable where TYpe : MonoBehaviour
{
    private static TYpe instance;

    public static TYpe Instance
    {
        get //instance��null�o�Ȃ����Ƃ��A�T�[�g���A�C���X�^���X��Ԃ�
        {
            //�A�T�[�g(Assert)�́A�v���O�����̎��s���ɓ���̏��������藧���Ă��邱�Ƃ����؂��邽�߂̎d�g��
            //�����null�̏ꍇ�A�G���[��\�����v���O�����̎��s���~����B
            Assert.IsNotNull(instance, "There is no object attached " + typeof(TYpe).Name);
            return instance;
        }
    }

    //��ԍŏ��Ɏ����Ŏ��s�����
    private void Awake()
    {
        if (instance != null && instance.gameObject != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this as TYpe;
        onAwakeProcess();
    }

    //Singleton�����݂��邩
    public static bool isExist()
    {
        return instance != null ? true : false;
    }

    //Destroy�������Ɏ��s����
    private void onDestroy()
    {
        if (instance != (this as TYpe)) return;
        onDestroyProcess();
        Dispose();
    }

    //���g��j������
    public virtual void Dispose()
    {
        if (isExist() == true)
        {
            instance = null;
        }
    }

    /*���̊֐����������Ēǉ�������������*/
    //Destroy�������̒ǉ����������s
    protected virtual void onDestroyProcess() { }
    //�h����ł̏��������������s
    protected virtual void onAwakeProcess() { }
}

/* reference -> https://qiita.com/Cova8bitdot/items/29b7064c7472a6f34972 */

/*������
 *using System;
 *using UnityEngine;
 *using UnityEngine.Assertions;
 *
 *public class SoundManager : SingletonMonoBehaviour<SoundManager>
 *{
 *    (����)
 *}
 */

/*
 * IDisposable�́A���\�[�X�̊J����N���[���A�b�v���s�����߂̃C���^�[�t�F�C�X�B
 * GC(�K�x�[�W�R���N�^)���I�u�W�F�N�g���������܂ł̊ԁA�A���}�l�[�W�h���\�[�X(�l�b�g���[�N�ڑ��Ȃ�)���������邱�Ƃ�ۏ؂���
 */
