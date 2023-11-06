#!/bin/bash

rm datasets/seadronesee/labels/val.cache 2> /dev/null
mv datasets/seadronesee/labels/val_tfm datasets/seadronesee/labels/val

# from model:
LIBPATH=lib/yolov5-master
MODELPATH=$LIBPATH/yolov5s.pt
DATASET=seadronesee
NAME=coco_pretrained_sds_intermediate
EPOCHS=10
PATIENCE=10

python $LIBPATH/train.py \
    --data datasets/$DATASET/data_tfm.yaml \
    --name $NAME \
    --weights $LIBPATH/yolov5s.pt \
    --optimizer Adam \
    --freeze 10 \
    --patience $PATIENCE \
    --epochs $EPOCHS \
    --hyp $LIBPATH/data/hyps/hyp.VOC.yaml

MODELPATH=$LIBPATH/runs/train/$NAME/weights/best.pt
NAME=coco_pretrained_sds
EPOCHS=20 # TODO 
PATIENCE=10

python $LIBPATH/train.py \
    --data datasets/$DATASET/data_tfm.yaml \
    --name $NAME \
    --weights $MODELPATH \
    --optimizer Adam \
    --patience $PATIENCE \
    --epochs $EPOCHS \
    --hyp $LIBPATH/data/hyps/hyp.VOC2.yaml 

echo ".... renaming....."
mv datasets/seadronesee/labels/val datasets/seadronesee/labels/val_tfm
echo "ENDED WITH SUCCESS!"
