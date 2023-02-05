import os
import pandas as pd
import matplotlib.pyplot as plt

DIR = os.path.join("/Users","jorge", "prueba-crest", "dataset")
SUPERSIZE = 4

def foo():
    for i in range(24):
        open("screenshot{}.txt".format(i),"w")

def get_line(idx, row, label):
    im_path = os.path.join(DIR, "screenshot{}.png".format(int(idx)))
    im = plt.imread(im_path)
    im_h, im_w, _ = im.shape

    xmax, ymax, xmin, ymin = row * SUPERSIZE
    ymax = im_h - ymax 
    ymin = im_h - ymin # true ymax

    # add bbox
    w = abs(xmax - xmin) / im_w
    h = abs(ymax - ymin) / im_h

    xc = (xmax + xmin) / 2 #TODO revisar
    yc = (ymax + ymin) / 2

    xc = xc / im_w
    yc = yc / im_h

    return "{} {} {} {} {}\n".format(label, xc, yc, w, h)
def main():
    df = pd.read_csv(os.path.join("dataset","debug.txt"), header=None)
    df = df.set_index(0)
    for group_idx, group_df in df.groupby(df.index):
        print("-----")
        print(group_idx)
        print("-----")
        with open("yolo/labels/screenshot{}.txt".format(group_idx), "w") as f:
            label = 0 # TODO
            for row_idx, row in group_df.iterrows():
                print(row_idx)
                lines = get_line(group_idx, row, label)
                f.writelines(lines)
                label +=1

if __name__ == "__main__":
    main()