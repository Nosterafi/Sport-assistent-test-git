using System.Windows.Forms;
using System.Drawing;

namespace VisualEffectsTests
{
    //����� ��� ������ VisualEffects.
    [TestFixture]
    public class VisualEffectsTests
    {
        //�������� ����� � ������������ ���� VisualEffects.actualForm. ����� ��������.
        //����� ���������� ���� ������ ��������� ������ �� �����, � ������� ����� �����������
        //��� ������.
        [Test]
        public void SettingLinkToTheMainForm()
        {
            var actualForm = new Form();
            VisualEffects.ActualForm = actualForm;
            Assert.AreEqual(VisualEffects.ActualForm, actualForm);
            VisualEffects.CancelChanged();
        }

        //������� ���������� ��������� �������� ���� VisualEffects.actualForm.
        //�������� ���� ������ ������ Exception � �������� �������� ���� ��� ���������.
        [Test]
        public void ChangeValueActualForm()
        {
            var actualForm = new Form();
            VisualEffects.ActualForm = actualForm;
            try
            {
                VisualEffects.ActualForm=new Form();
            }
            catch 
            { }
            Assert.AreEqual(VisualEffects.ActualForm, actualForm);
            VisualEffects.CancelChanged();
        }

        //������� �������� null �� newControl.
        //����� ControlsChange ������ ������ Exception.
        [Test]
        public void �hangeNull()
        {
            var actualForm = new Form();
            VisualEffects.ActualForm = actualForm;
            try
            {
                VisualEffects.ControlsChange(null, new UserControl(), new Point(0, 0));
                throw new Exception("����� ControlsChange ������ ��� ������ ������");
            }
            catch { }
            VisualEffects.CancelChanged();
        }

        //������� �������� ������� Control �� null.
        //ControlsChange ������ ������ Exception.
        [Test]
        public void ChangeToNull()
        {
            var a = new Form();
            VisualEffects.ActualForm = a;
            try
            {
                VisualEffects.ControlsChange(new UserControl(), null, new Point(0, 0));
                throw new Exception("����� ControlsChange ������ ��� ������ ������");
            }
            catch { }
            VisualEffects.CancelChanged();
        }

        //����� Control-�� ��� ������ �� ������� ������.
        //����� ������ ������ ControlsChange �����  ������ Control ����� ����� �� ��������� Location � Anchor,
        //��� � ������.
        [Test]
        public void CorrectChangeWithLocation()
        {
            var a = new Form();
            VisualEffects.ActualForm=a;
            var oldControl = new UserControl();
            a.Controls.Add(oldControl);
            VisualEffects.ControlsChange(oldControl, new UserControl(), new Point(30, 30));
            Assert.AreEqual(a.Controls[0].Location, new Point(30, 30));
            Assert.AreEqual(a.Controls[0].Anchor, AnchorStyles.None);
            VisualEffects.CancelChanged();
        }

        //����� Control-�� ��� ������ �� ������� ������.
        //����� ������ ������ ControlsChange �����  ������ Control ����� ����� �� ��������� Location, Size � Anchor,
        //��� � ������ (��� ��� ����� ����������� ���������� ��� newLocation).
        [Test]
        public void CorrectChangeWithoutLocation()
        {
            var a = new Form();
            VisualEffects.ActualForm = a;
            var oldControl = new UserControl();
            a.Controls.Add(oldControl);
            VisualEffects.ControlsChange(oldControl, new UserControl());
            Assert.AreEqual(a.Controls[0].Location, oldControl.Location);
            Assert.AreEqual(a.Controls[0].Anchor, AnchorStyles.None);
            VisualEffects.CancelChanged();
        }

        
        [Test]
        public void CorrectHiding()
        {
            var controls = new UserControl[5];
            for(int i=0; i<controls.Length; i++)
            {
                controls[i] = new UserControl();
            }
            VisualEffects.HideControls(controls);
            foreach(var control in controls)
            {
                Assert.AreEqual(control.Visible, false);
            }
            VisualEffects.CancelChanged();
        }

        public void WhenArgumentInHideControlsIsNull()
        {
            try
            {
                VisualEffects.HideControls(null);
            }
            catch (NullReferenceException e) { }
            VisualEffects.CancelChanged();
        }

        [Test]
        public void CorrectClear()
        {
            var a = new Form();
            VisualEffects.ActualForm = a;
            VisualEffects.ActualForm.Controls.Add (new UserControl());
            VisualEffects.Clear();
            Assert.AreEqual(a.Controls.Count, 0);
            VisualEffects.CancelChanged();
        }
    }
}