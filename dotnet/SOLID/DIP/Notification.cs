using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOSOLID_CLOSELY_BOUND
{
    public class Email
    {
        public void Send()
        {
            // код для отправки email-письма
        }
    }

    // Уведомление
    public class Notification
    {
        private Email email;
        public Notification()
        {
            email = new Email();
        }

        public void EmailDistribution()
        {
            email.Send();
        }
    }
}

namespace NOSOLID_WEAK_BOUND
{
    public interface IMessenger
    {
        void Send();
    }

    public class Email : IMessenger
    {
        public void Send()
        {
            // код для отправки email-письма
        }
    }

    public class SMS : IMessenger
    {
        public void Send()
        {
            // код для отправки SMS
        }
    }

    // Уведомление
    public class Notification
    {
        private IMessenger _messenger;
        public Notification()
        {
            _messenger = new Email();
        }

        public void DoNotify()
        {
            _messenger.Send();
        }
    }

}

namespace SOLID_CONSTRUCTOR_INJECTION
{
    public class Notification
    {
        private NOSOLID_WEAK_BOUND.IMessenger _messenger;
        public Notification(NOSOLID_WEAK_BOUND.IMessenger mes)
        {
            _messenger = mes;
        }

        public void DoNotify()
        {
            _messenger.Send();
        }
    }
}

namespace SOLID_PROPERTY_INJECTION
{

    public class Notification
    {
        private NOSOLID_WEAK_BOUND.IMessenger _messenger;
        public Notification()
        {

        }

        public NOSOLID_WEAK_BOUND.IMessenger Messanger
        {
            set
            {
                _messenger = value;
            }
        }

        public void DoNotify()
        {
            _messenger.Send();
        }
    }
}

namespace SOLID_METHOD_INJECTION
{

    public class Notification
    {
        public void DoNotify(NOSOLID_WEAK_BOUND.IMessenger mes)
        {
            mes.Send();
        }
    }
}
