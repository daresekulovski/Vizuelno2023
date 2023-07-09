using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BrickBreaker
{
    public class Scene
    {
        public Form1 form1;
        public Platform Platform { get; set; }
        public List<Ball> Balls { get; set; }
        public List<Brick> Bricks { get; set; }
        public List<Buff> Buffs { get; set; }
        public int MaxBuffs { get; set; }
        public int SpawnedBuffs { get; set; }
        public bool IsResizeBuff { get; set; }
        public int AttemptsLeft { get; set; }
        public int buffDuration { get; set; }
        public int buffTime { get; set; }
        public int Speed { get; set; }
        public int Score { get; set; }
        public Scene()
        {
            Platform = new Platform();
            Balls = new List<Ball>();
            Balls.Add(new Ball());
            Bricks = new List<Brick>();
            while(Bricks.Count < 40)
            {
                Brick brick = new Brick();
                bool IsIntersecting = false;
                foreach (var item in Bricks)
                {
                    if (item.Intersect(brick))
                    {
                        IsIntersecting = true;
                        break;
                    }
                }
                if (!IsIntersecting)
                {
                    Bricks.Add(brick);
                }
            }
            Buffs = new List<Buff>();
            MaxBuffs = 5;
            AttemptsLeft = 3;
            buffTime = 0;
            buffDuration = 300;
            IsResizeBuff = false;
            Speed = 0;
            Score = 0;
        }
        public void Draw(Graphics g)
        {
            Platform.Draw(g);
            foreach (var item in Balls)
            {
                item.Draw(g);
            }
            foreach (var item in Bricks)
            {
                item.Draw(g);
            }
            foreach (var item in Buffs)
            {
                item.Draw(g);
            }
        }
        public void PlatformMove(int currX)
        {
            Platform.Move(currX);
        }
        public void AdjustBall(int currX)
        {
            if (Balls.Count > 0 && !Balls[0].IsMoving && !Platform.AtEdge)
            {
                Balls[0].AdjustBall(currX);
            }
        }
        public void MoveBalls()
        {
            foreach (var ball in Balls)
            {
                ball.Move(Platform, Bricks);
            }
            if (IsResizeBuff)
            {
                buffTime++;
                if (buffTime >= buffDuration)
                {
                    Platform.ResetSize();
                    buffTime = 0;
                    IsResizeBuff = false;
                }
            }
        }
        public void DestroyBrick()
        {
            for (int i = Bricks.Count  - 1; i >= 0; i--)
            {
                if (Bricks[i].Lives == -1)
                {
                    ChanceToGenerateBuff(Bricks[i]);
                    Bricks.RemoveAt(i);
                    Score += 100;
                    if (Bricks.Count % 10 == 0)
                    {
                        form1.SpeedUp();
                    }
                }
            }
        }
        public void ChanceToGenerateBuff(Brick brick)
        {
            Random random = new Random();
            if (SpawnedBuffs <= MaxBuffs && random.Next(0, 101) % 6 == 0)
            {
                int rng = random.Next(0, 101);
                Buff buff;
                if (rng <= 25)
                {
                    buff = new Buff(brick.Center, Color.Pink);
                }
                else if (rng >= 26 && rng <= 50)
                {
                    buff = new Buff(brick.Center, Color.Purple);
                }
                else if (rng >= 51 && rng <= 75)
                {
                    buff = new Buff(brick.Center, Color.Orange);
                }
                else
                {
                    buff = new Buff(brick.Center, Color.Blue);
                }
                Buffs.Add(buff);
                SpawnedBuffs++;
            }
        }
        public void MoveBuff()
        {
            foreach (var buff in Buffs)
            {
                buff.Move();
            }
        }
        public void ConsumeBuff()
        {
            for (int i = Buffs.Count - 1; i >= 0; i--)
            {
                if (Buffs[i].Consumed(Platform))
                {
                    if (Buffs[i].Color == Color.Pink || Buffs[i].Color == Color.Purple)
                    {
                        Platform.Resize(Buffs[i].Color);
                        IsResizeBuff = true;
                    }
                    else if (Buffs[i].Color == Color.Orange)
                    {
                        AttemptsLeft++;
                    }
                    else
                    {
                        Ball ball = new Ball(Balls[0].Center);
                        Ball ball1 = new Ball(Balls[0].Center);
                        while (ball.Angle == ball1.Angle)
                        {
                            ball1 = new Ball(Balls[0].Center);
                        }
                        Balls.Add(ball);
                        Balls.Add(ball1);
                    }
                    Buffs.RemoveAt(i);
                }
            }
        }
        public void Passed()
        {
            for (int i = Balls.Count - 1; i >= 0; i--)
            {
                if (Balls[i].Center.Y >= 573 + Balls[i].Radius)
                {
                    Balls.RemoveAt(i);
                }
            }
            for (int i = Buffs.Count - 1; i>= 0; i--)
            {
                if (Buffs[i].Center.Y >= 573 + Buffs[i].Radius)
                {
                    Buffs.RemoveAt(i);
                }
            }
        }
        public void AnotherTry()
        {
            if (Balls.Count == 0 && AttemptsLeft > 0)
            {
                AttemptsLeft--;
                Ball ball = new Ball();
                ball.Center = new Point(Platform.Center.X, ball.Center.Y);
                Balls.Add(ball);
                form1.timerMove.Stop();
            }
        }
    }
}
