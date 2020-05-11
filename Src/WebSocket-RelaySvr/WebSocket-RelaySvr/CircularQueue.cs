using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocket_RelaySvr
{
    class CircularQueue
    {
        public CircularQueue()
        {
            m_front = 0;
            m_rear = 0;
            m_buff = new byte[1024];
            SIZE = 1024;
        }


        // push buffer
        public int Push(byte[] buff, int size)
        {
            int remainSize = SIZE - Size();
            if (remainSize < size)
                return 0;

            int totalCpSize = 0;
            int maxSize = (m_front > m_rear) ? (m_front - m_rear - 1) : (SIZE - m_rear);
            int cpSize = Math.Min(maxSize, size);
            Buffer.BlockCopy(buff, 0, m_buff, m_rear, cpSize);
            totalCpSize += cpSize;

            if ((cpSize < size) && (m_rear >= m_front))
            {
                // copy remain data
                int remainSize2 = Math.Max(0, m_front - 1);
                int remainCpSize = Math.Min(remainSize2, size - cpSize);
                Buffer.BlockCopy(buff, cpSize, m_buff, 0, remainCpSize);
                totalCpSize += remainCpSize;

                m_rear = remainCpSize;
                m_rear %= SIZE;
            }
            else
            {
                m_rear += cpSize;
                m_rear %= SIZE;
            }

            return totalCpSize;
        }


        // pop size
        public bool Pop(int size)
        {
            if (Empty())
                return false;
            int curSize = Size();
            if (curSize < size)
                return false;

            if (m_front < m_rear)
            {
                int popSize = Math.Min(curSize, size);
                m_front += popSize;
                m_front %= SIZE;
                return popSize == size;
            }
            else
            {
                int popSize = Math.Min((SIZE - m_front), size);
                m_front += popSize;
                m_front %= SIZE;

                if (popSize < size)
                {
                    int popRemainSize = Math.Min(m_rear, size - popSize);
                    m_front = popRemainSize;
                    m_front %= SIZE;
                    return size == (popSize + popRemainSize);
                }
            }

            return true;
        }


        // copy front
        public bool FrontCopy(byte[] dst, int size)
        {
            if (Empty())
                return false;
            int curSize = Size();
            if (curSize < size)
                return false;

            if (m_front < m_rear)
            {
                int cpSize = Math.Min(curSize, size);
                Buffer.BlockCopy(m_buff, m_front, dst, 0, cpSize);
                return cpSize == size;
            }
            else
            {
                int cpSize = Math.Min((SIZE - m_front), size);
                Buffer.BlockCopy(m_buff, m_front, dst, 0, cpSize);

                if (cpSize < size)
                {
                    int cpRemainSize = Math.Min(m_rear, size - cpSize);
                    Buffer.BlockCopy(m_buff, 0, dst, cpSize, cpRemainSize);
                    return size == (cpSize + cpRemainSize);
                }
            }

            return true;
        }


        // pop size
        public bool Pop(byte[] dst, int size)
        {
            if (Empty())
                return false;
            int curSize = Size();
            if (curSize < size)
                return false;

            if (m_front < m_rear)
            {
                int popSize = Math.Min(curSize, size);
                Buffer.BlockCopy(m_buff, m_front, dst, 0, popSize);
                m_front += popSize;
                m_front %= SIZE;
                return popSize == size;
            }
            else
            {
                int popSize = Math.Min(SIZE - m_front, size);
                Buffer.BlockCopy(m_buff, m_front, dst, 0, popSize);
                m_front += popSize;
                m_front %= SIZE;

                if (popSize < size)
                {
                    int popRemainSize = Math.Min(m_rear, size - popSize);
                    Buffer.BlockCopy(m_buff, 0, dst, popSize, popRemainSize);
                    m_front = popRemainSize;
                    m_front %= SIZE;
                    return size == (popSize + popRemainSize);
                }
            }
            return true;
        }


        // is empty?
        public bool Empty()
        {
            return m_front == m_rear;
        }


        // return size of buffer
        public int Size()
        {
            if (m_front > m_rear)
            {
                return ((SIZE - m_front) + m_rear);
            }
            return (m_rear - m_front);
        }


        // clear
        public void Clear()
        {
            m_front = m_rear = 0;
        }


        public byte[] m_buff;
        public int m_front;
        public int m_rear;
        public int SIZE;
    }
}
