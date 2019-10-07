using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Framework
{
    public class StringManipulator
    {


        int version = 0;
        Stats myStats = new Stats();
        LinkedList<Section> sections = new LinkedList<Section>();

        public int Length
        {
            get
            {
                var savedLength = GetOrMakeStat((s) => s.Length, MakeAllStats).Value;

                return savedLength;

            }
        }

        void MakeAllStats(Stats stats)
        {
            StringBuilder sb = new StringBuilder(this.Length);
            foreach (Section section in sections)
            {
                sb.Append(section.Base, section.Start, section.Length);
            }

            stats.Value = sb.ToString();
            stats.version = this.version;
            stats.Length = stats.Value.Length;
        }

        public void Add(string value)
        {
            lock (this)
            {
                sections.AddLast(new Section(value));
                version++;
            }
        }

        public void Insert(string value, int position)
        {
            lock (this)
            {
                int positionsSoFar = 0;
                var node = this.sections.First;
                do
                {
                    var section = node.Value;
                    positionsSoFar += section.Length;

                    if (positionsSoFar >= position) break;

                } while ((node = node.Next) != null);

                if (positionsSoFar > position)
                {

                    // qwerty|asdfg|
                    var toSplit = node.Value;


                    int lenToSplitPoint = positionsSoFar - position;


                    toSplit.Length -= lenToSplitPoint;
                    var newSplitBit = new Section(toSplit.Base, toSplit.Start + toSplit.Length, lenToSplitPoint);
                    sections.AddAfter(node, newSplitBit);

                    var insertedSection = new Section(value);
                    sections.AddAfter(node, insertedSection);


                }
                else if (positionsSoFar < position)
                {
                    //insert a new section at the end
                    this.Add(value);
                }
                else
                {
                    //cleanly fits between 2 sections
                    var insertedSection = new Section(value);
                    sections.AddAfter(node, insertedSection);


                }
            }

        }

        public string Substring(int start, int length)
        {
            char[] result = new char[length];


            int offsetOfNode = 0;
            int foundCount = 0;
            var node = this.sections.First;

            while (node != null)
            {
                var section = node.Value;


                if (section.Start < start)
                {
                    if ((section.Start + section.Length) > start)
                    {
                        //this is the section the right section to start
                        for (int x = foundCount > 0 ? 0 : (start - section.Start); x < section.Length && foundCount < length; x++)
                        {
                            result[foundCount++] = section.Base[section.Start + x];
                        }

                    }

                    if (foundCount >= length)
                    {
                        //found it all
                        break;
                    }

                    // we need the next section
                    node = node.Next;
                    continue;
                }



            }
            return new string(result);

        }


        public void Remove(string value, string replaceWith = "")
        {
            int foundLetters = 0;
            bool isFinding = false;

            int sectionsToGoBack = 0;
            var node = sections.First;
            while (node != null)
            {
                var section = node.Value;

                // if (section.Length == 0) continue;

                var foundAt = section.Start;
                var sbase = section.Base;

                if (!isFinding)
                {
                    foundLetters = 0;
                    foundAt = section.Start;

                    try
                    {
                        foundAt = sbase.IndexOf(value[0], foundAt, section.Length);
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                    if (foundAt > -1)
                    {
                        //found the first character so starting looping through to find the rest
                        isFinding = true;
                        foundLetters++;

                        //keep search till a character mismatch or the end of the section
                        while (isFinding
                            && foundLetters < value.Length
                            && (foundAt + foundLetters) < section.Start + section.Length)
                        {
                            //check for mismatch
                            if (sbase[foundAt + foundLetters] != value[foundLetters])
                            {
                                //swing and a miss. 
                                isFinding = false;
                                foundLetters = 0;
                                foundAt++;
                                break;
                            }

                            foundLetters++;
                        }

                        //if isFinding then it was end of section or found it all
                        if (isFinding)
                        {
                            //check if found it all
                            if (foundLetters == value.Length)
                            {
                                var newStart = (foundAt + foundLetters);
                                var newLength = section.Length - (newStart - section.Start);
                                if (newLength > 0)
                                {
                                    sections.AddAfter(node, new Section(section.Base, newStart, newLength));


                                }

                                section.Length = foundAt - section.Start;

                                if (section.Length == 0)
                                {
                                    if (node.Previous == null)
                                    {
                                        //this was the first node
                                        sections.RemoveFirst();

                                        if (!string.IsNullOrEmpty(replaceWith))
                                        {
                                            //insert new stuff to the beginning, the loop will move us past it
                                            sections.AddFirst(new Section(replaceWith, 0, replaceWith.Length));
                                        }
                                        node = sections.First;
                                    }
                                    else
                                    {
                                        var temp = node.Previous;
                                        sections.Remove(node);
                                        node = temp;
                                        if (!string.IsNullOrEmpty(replaceWith))
                                        {
                                            //insert new stuff before where we are so we dont search that text
                                            sections.AddAfter(node, new Section(replaceWith, 0, replaceWith.Length));
                                            node = node.Next;
                                        }

                                    }

                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(replaceWith))
                                    {
                                        //insert new stuff before where we are so we dont search that text
                                        sections.AddAfter(node, new Section(replaceWith, 0, replaceWith.Length));
                                        //move us on to the new node so the loop moves us past it
                                        node = node.Next;
                                    }

                                }
                                isFinding = false;
                            }
                            else
                            {
                                //must be crossing a section boundary
                                sectionsToGoBack = 1;
                            }
                        }

                    }
                }
                else
                {
                    int pointerInThisSection = section.Start;
                    while (isFinding
                            && foundLetters < value.Length
                            && (pointerInThisSection) < section.Length)
                    {
                        if (sbase[section.Start + pointerInThisSection] != value[foundLetters])
                        {
                            //swing and a miss. 
                            isFinding = false;
                            foundLetters = 0;
                            foundAt++;
                            break;
                        }
                        pointerInThisSection++;
                        foundLetters++;
                    }
                    if (isFinding)
                    {
                        if (foundLetters == value.Length)
                        {
                            //trim the beginning of this sections
                            section.Start += pointerInThisSection;
                            section.Length -= pointerInThisSection;

                            int charsInFirstSection = foundLetters - pointerInThisSection;
                            for (int x = 1; x < sectionsToGoBack; x++)
                            {
                                charsInFirstSection -= node.Previous.Value.Length;
                                sections.Remove(node.Previous);
                            }

                            if (section.Length < 1 || section.Start >= section.Base.Length)
                            {
                                var oldLast = node.Previous;
                                sections.Remove(node);
                                node = oldLast;
                            }

                            //trim end of previous node
                            if (charsInFirstSection > 0)
                            {
                                node.Previous.Value.Length -= charsInFirstSection;
                            }


                        }
                        else
                        {
                            //must be crossing a section boundary
                            sectionsToGoBack += 1;
                        }
                    }
                }
                node = node.Next;
            };
        }

        public override string ToString()
        {
            return GetOrMakeStat<string>((s) => s.Value, MakeAllStats);
        }

        private T GetOrMakeStat<T>(Func<Stats, T> getStat, Action<Stats> makeStat)
        {
            lock (this)
            {
                if (myStats.version != version)
                {
                    myStats = new Stats() { version = version };
                }
                T stat = getStat(myStats);
                if (stat == null)
                {
                    makeStat(myStats);
                    stat = getStat(myStats);
                }
                return stat;


            }
        }


        public int IndexOf(string value, int startIndex = 0)
        {
            int foundLetters = 0;
            bool isFinding = false;

            int sectionsToGoBack = 0;
            int originalFoundLocation = 0;
            int stringPassedInPreviousSections = 0;
            var node = this.sections.First;

            while (node != null)
            {
                var section = node.Value;

                var foundAt = section.Start;
                var sbase = section.Base;

                if (!isFinding)
                {
                    foundLetters = 0;
                    foundAt = section.Start + (Math.Max(startIndex - stringPassedInPreviousSections, 0));

                    if (
                        (foundAt < sbase.Length)
                        &&
                        (foundAt = sbase.IndexOf(value[0], foundAt, (section.Length - (foundAt - section.Start)))) > -1
                       )
                    {
                        //found the first character so starting looping through to find the rest
                        isFinding = true;

                        //record where we started finding;
                        originalFoundLocation = stringPassedInPreviousSections + foundAt - section.Start;

                        foundLetters++;
                        while (isFinding
                            && foundLetters < value.Length
                            && (foundAt + foundLetters) < section.Start + section.Length)
                        {
                            if (sbase[foundAt + foundLetters] != value[foundLetters])
                            {
                                //swing and a miss. 
                                isFinding = false;
                                foundLetters = 0;
                                foundAt++;
                                break;
                            }

                            foundLetters++;
                        }
                        if (isFinding)
                        {
                            if (foundLetters == value.Length)
                            {
                                //finished finding - shortcut return
                                return originalFoundLocation;
                            }
                            else
                            {
                                //must be crossing a section boundary
                                sectionsToGoBack = 1;
                            }
                        }

                    }
                }
                else
                {
                    int pointerInThisSection = section.Start;
                    while (isFinding
                            && foundLetters < value.Length
                            && (pointerInThisSection) < section.Length)
                    {
                        if (sbase[section.Start + pointerInThisSection] != value[foundLetters])
                        {
                            //swing and a miss. 
                            isFinding = false;
                            foundLetters = 0;
                            foundAt++;
                            break;
                        }
                        pointerInThisSection++;
                        foundLetters++;
                    }
                    if (isFinding)
                    {
                        //Completed Finding the text
                        if (foundLetters == value.Length)
                        {
                            return originalFoundLocation;

                        }
                        else
                        {
                            //must be crossing a section boundary
                            sectionsToGoBack += 1;
                        }
                    }
                }


                stringPassedInPreviousSections += node.Value.Length;
                node = node.Next;

            }
            return -1;
        }

        class Stats
        {
            public int? version = 0;
            public int? Length = 0;
            public string Value = null;
        }




        class Section
        {


            public Section(string theBase, int start = 0, int? length = null)
            {
                this.Base = theBase;
                this.Start = start;
                this.Length = length.HasValue ? length.Value : theBase.Length - start;
            }

            public int Length { get; set; }

            public String Base { get; set; }
            public int Start { get; set; }

            public override string ToString()
            {
                return new string(Base.ToCharArray(), Start, Length);
            }
        }



    }
}
